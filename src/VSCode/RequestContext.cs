using System;

using VSCode.JsonRpc;

namespace VSCode
{
    public class RequestContext
    {
        private LanguageServer _server;

        internal RequestContext(RequestMessage request, LanguageServer server)
        {
            _server = server;

            Request = request;
        }

        public event EventHandler RequestCancelled;

        public RequestMessage Request { get; private set; }

        internal void Cancel()
        {
            RequestCancelled?.Invoke(this, new EventArgs());
        }

        public void SendError(object error)
        {
            _server.SendResponseAsync(Request.Id, null, error).Wait();
        }

        public void SendResult(object result)
        {
            _server.SendResponseAsync(Request.Id, result).Wait();
        }
    }
}
