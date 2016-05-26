using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using VSCode.JsonRpc;
using static VSCode.Utils.DebugLogger;

namespace VSCode
{
    /// <summary>
    /// A Visual Studio Code language server implementation.
    /// </summary>
    /// <example>
    /// <code language="cs">
    /// using (LanguageServer server = new LanguageServer())
    /// {
    ///     server.Start();
    ///     server.Editor.ShowMessage(MessageType.Info, "Hello from .NET!");
    /// }
    /// </code>
    /// </example>
    public partial class LanguageServer : IDisposable
    {
        private int _currentId;
        private List<IFeature> _features;
        private IMessageReader _messageReader;
        private IMessageWriter _messageWriter;
        private Dictionary<int, ResponseMessage> _responses;
        private ServerCapabilities _serverCapabilities;
        private CancellationTokenSource _tokenSource;

        private List<RequestContext> _requestContexts;

        public LanguageServer()
        {
            _features = new List<IFeature>();
            _messageReader = new StreamMessageReader(Console.OpenStandardInput(), Encoding.UTF8);
            _messageWriter = new StreamMessageWriter(Console.OpenStandardOutput(), Encoding.UTF8);
            _requestContexts = new List<RequestContext>();
            _responses = new Dictionary<int, ResponseMessage>();
            _tokenSource = new CancellationTokenSource();

            _serverCapabilities = new ServerCapabilities();
        }

        public LanguageServer(ServerCapabilities capabilities)
            : this()
        {
            _serverCapabilities = capabilities;
            _serverCapabilities.TextDocumentSync = TextDocumentSyncKind.Incremental;
        }

        public LanguageServerState State { get; private set; }

        public event EventHandler Exit;

        public event EventHandler<InitializeParams> Initialize;

        /// <summary>
        /// An event fired when notifications are received from the client.
        /// </summary>
        public event EventHandler<NotificationMessage> NotificationReceived;

        /// <summary>
        /// An event fired when requests are received from the client.
        /// </summary>
        public event EventHandler<RequestContext> RequestReceived;

        /// <summary>
        /// An event fired when responses are received from the client. The <code>SendRequestAsync()</code> method should be used instead of listening to this event when sending normal requests.
        /// </summary>
        public event EventHandler<ResponseMessage> ResponseReceived;

        public void Dispose()
        {
            foreach (IFeature feature in _features)
            {
                try
                {
                    feature.Dispose();
                }

                catch (Exception ex)
                {
                    Debug(ex);
                }
            }

            Stop();
        }

        public T GetFeature<T>()
            where T : IFeature
        {
            IFeature feature = _features.Where(x => x.GetType().Equals(typeof(T))).FirstOrDefault();

            if (feature == null)
            {
                feature = Activator.CreateInstance<T>();

                try
                {
                    feature.Initialize(this);
                }

                catch (Exception ex)
                {
                    throw new FeatureActivationException(ex);
                }

                _features.Add(feature);
            }

            return (T)feature;
        }

        /// <summary>
        /// Sends a notification message to the client.
        /// </summary>
        /// <param name="method">The method to invoke on the client.</param>
        /// <param name="parameters">The method paramters (if applicable) or null.</param>
        public void SendNotification(string method, object parameters)
        {
            if (State != LanguageServerState.Started)
            {
                throw new InvalidOperationException("The language server must be started before sending a notification.");
            }

            NotificationMessage notification = new NotificationMessage
            {
                Method = method,
                Params = parameters.ToJObject()
            };

            Debug("Send Notification", notification);
            _messageWriter.WriteAsync(notification);
        }

        /// <summary>
        /// Sends a notification message to the client.
        /// </summary>
        /// <param name="method">The method to invoke on the client.</param>
        public void SendNotification(string method)
        {
            SendNotification(method, null);
        }

        /// <summary>
        /// Sends a request message to the client and awaits a response.
        /// </summary>
        /// <param name="method">The method to invoke on the client.</param>
        /// <param name="parameters">The method paramters (if applicable) or null.</param>
        /// <returns></returns>
        public async Task<ResponseMessage> SendRequestAsync(string method, object parameters)
        {
            if (State != LanguageServerState.Started)
            {
                throw new InvalidOperationException("The language server must be started before sending a request.");
            }

            int id = _currentId++;

            RequestMessage request = new RequestMessage
            {
                Id = id,
                Method = method,
                Params = parameters.ToJObject()
            };

            Debug("Send Request", request);
            await _messageWriter.WriteAsync(request);

            _responses.Add(id, null);

            return await Task.Factory.StartNew(() =>
            {
                ResponseMessage response;

                while (true)
                {
                    Task.Delay(2).Wait();

                    response = _responses[id];

                    if (response != null)
                    {
                        break;
                    }
                }

                return response;
            });
        }

        /// <summary>
        /// Sends a request message to the client and awaits a response.
        /// </summary>
        /// <param name="method">The method to invoke on the client.</param>
        /// <returns></returns>
        public async Task<ResponseMessage> SendRequestAsync(string method)
        {
            return await SendRequestAsync(method, null);
        }

        /// <summary>
        /// Sends a response to the client using the provided ID, result, and error.
        /// </summary>
        /// <param name="id">The ID that corresponds to the request the server is responding to.</param>
        /// <param name="result">The result object.</param>
        /// <param name="error">An object representing an error or null if no error is applicable.</param>
        /// <returns></returns>
        public async Task SendResponseAsync(int id, object result, object error)
        {
            if (State != LanguageServerState.Started)
            {
                throw new InvalidOperationException("The language server must be started before sending a response.");
            }

            RequestContext context = _requestContexts.Where(x => x.Request.Id.Equals(id)).FirstOrDefault();

            if (context == null)
            {
                return;
            }

            _requestContexts.Remove(context);

            ResponseMessage response = new ResponseMessage
            {
                Id = id,
                Error = error.ToJObject(),
                Result = result.ToJObject()
            };

            Debug("Send Response", response);
            await _messageWriter.WriteAsync(response);
        }

        /// <summary>
        /// Sends a response to the client using the provided ID and result.
        /// </summary>
        /// <param name="id">The ID that corresponds to the request the server is responding to.</param>
        /// <param name="result">The result object.</param>
        /// <returns></returns>
        public async Task SendResponseAsync(int id, object result)
        {
            await SendResponseAsync(id, result, null);
        }

        /// <summary>
        /// Starts the language server (enables sending and receiving messages).
        /// </summary>
        public void Start()
        {
            State = LanguageServerState.WaitingForInitialization;
            Task.Factory.StartNew(_RunMessageLoop, _tokenSource.Token);
        }

        /// <summary>
        /// Stops the language server.
        /// </summary>
        public void Stop()
        {
            if (State != LanguageServerState.Stopped)
            {
                _tokenSource.Cancel();
                State = LanguageServerState.Stopped;
            }
        }

        public void WaitForState(LanguageServerState desiredState, TimeSpan timeout)
        {
            DateTime expires = DateTime.Now.Add(timeout);

            while (DateTime.Now < expires)
            {
                Task.Delay(2).Wait();

                if (State == desiredState)
                {
                    break;
                }
            }
        }

        public void WaitForState(LanguageServerState desiredState)
        {
            WaitForState(desiredState, TimeSpan.MaxValue);
        }

        private void _HandleMessage(IMessage message)
        {
            try
            {
                if (message.GetType().Equals(typeof(NotificationMessage)))
                {
                    _HandleNotification((NotificationMessage)message);
                }

                else if (message.GetType().Equals(typeof(RequestMessage)))
                {
                    _HandleRequest((RequestMessage)message);
                }

                else if (message.GetType().Equals(typeof(ResponseMessage)))
                {
                    _HandleResponse((ResponseMessage)message);
                }

                else
                {
                    Debug("Unhandled Message", message);
                }
            }

            catch (Exception ex)
            {
                Debug(ex);
            }
        }

        private void _HandleNotification(NotificationMessage message)
        {
            Debug("Handle Notification", message);

            if (message.Method.Equals(CoreMethods.Exit))
            {
                Stop();
                Exit?.Invoke(this, new EventArgs());
            }

            else
            {
                NotificationReceived?.Invoke(this, message);
            }
        }

        private void _HandleRequest(RequestMessage message)
        {
            Debug("Handle Request", message);

            RequestContext context = new RequestContext(message, this);
            _requestContexts.Add(context);

            if (message.Method.Equals(CoreMethods.CancelRequest))
            {
                // remove the duplicate context we just added
                _requestContexts.Remove(context);

                // get the original request context
                RequestContext cancelContext = _requestContexts.Where(x => x.Request.Id.Equals(message.Id)).FirstOrDefault();
                cancelContext?.Cancel();
            }

            else if (message.Method.Equals(CoreMethods.Initialize))
            {
                State = LanguageServerState.Started;

                InitializeParams parameters = message.Params.ToObject<InitializeParams>();
                Initialize?.Invoke(this, parameters);

                InitializeResult result = new InitializeResult
                {
                    Capabilities = _serverCapabilities
                };

                SendResponseAsync(message.Id, result).Wait();
            }

            else if (message.Method.Equals(CoreMethods.Shutdown))
            {
                SendResponseAsync(message.Id, null).Wait();
            }

            RequestReceived?.Invoke(this, context);
        }

        private void _HandleResponse(ResponseMessage message)
        {
            Debug("Handle Response", message);

            if (_responses.ContainsKey(message.Id))
            {
                _responses[message.Id] = message;
            }

            ResponseReceived?.Invoke(this, message);
        }

        private void _RunMessageLoop()
        {
            Debug("Message Loop Started");

            while (!_tokenSource.Token.IsCancellationRequested)
            {
                Task<IMessage> task = null;

                task = _messageReader.ReadAsync();
                task.Wait(_tokenSource.Token);

                IMessage message = task.Result;

                if (message != null)
                {
                    _HandleMessage(message);
                }
            }

            Debug("Message Loop Ended");
        }
    }
}
