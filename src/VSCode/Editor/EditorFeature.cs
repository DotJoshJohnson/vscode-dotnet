using System;
using System.Threading.Tasks;

using VSCode.JsonRpc;

namespace VSCode.Editor
{
    /// <summary>
    /// Provides editor-related features such as change detection and UI integration.
    /// </summary>
    public class EditorFeature : IFeature
    {
        private LanguageServer _server;

        /// <summary>
        /// Raised when the synchronized configuration section has been changed in VS Code.
        /// </summary>
        public event EventHandler<DidChangeConfigurationParams> ConfigurationChanged;

        /// <summary>
        /// Raised when the contents of a text document have been changed.
        /// </summary>
        public event EventHandler<DidChangeTextDocumentParams> TextDocumentChanged;

        /// <summary>
        /// Raised when a watched file has changed.
        /// </summary>
        public event EventHandler<DidChangeWatchedFilesParams> WatchedFilesChanged;

        /// <summary>
        /// Raised when a text document has been closed.
        /// </summary>
        public event EventHandler<DidCloseTextDocumentParams> TextDocumentClosed;

        /// <summary>
        /// Raised when a text document has been opened.
        /// </summary>
        public event EventHandler<DidOpenTextDocumentParams> TextDocumentOpened;

        /// <summary>
        /// Raised when a text document has been savedd.
        /// </summary>
        public event EventHandler<DidSaveTextDocumentParams> TextDocumentSaved;

        /// <summary>
        /// A virtual workspace representing the actual workspace in VS Code.
        /// <remarks>
        /// <note type="note">It is recommended that the <see cref="Workspace" /> property be used instead of listening to document change, open, and close events manually as it handles incremental synchronization automatically.</note>
        /// </remarks>
        /// </summary>
        public Workspace Workspace { get; private set; }

        /// <summary>
        /// See <see cref="IDisposable.Dispose" />.
        /// </summary>
        public void Dispose()
        {
            _server.NotificationReceived -= _HandleNotification;

            Workspace.Dispose();
        }

        /// <summary>
        /// See <see cref="IFeature.Initialize(LanguageServer)" />.
        /// </summary>
        /// <param name="languageServer"></param>
        public void Initialize(LanguageServer languageServer)
        {
            _server = languageServer;
            _server.NotificationReceived += _HandleNotification;

            Workspace = new Workspace(this);
        }

        /// <summary>
        /// Logs a message to the developer console in VS Code.
        /// </summary>
        /// <param name="type">The type of message to log.</param>
        /// <param name="message">The message text.</param>
        public void LogMessage(MessageType type, string message)
        {
            LogMessageParams parameters = new LogMessageParams
            {
                Message = message,
                Type = type
            };

            _server.SendNotification(EditorMethods.LogMessage, parameters);
        }

        /// <summary>
        /// Shows a message in the VS Code UI.
        /// </summary>
        /// <param name="type">The type of message to show.</param>
        /// <param name="message">The message text.</param>
        public void ShowMessage(MessageType type, string message)
        {
            ShowMessageParams parameters = new ShowMessageParams
            {
                Message = message,
                Type = type
            };

            _server.SendNotification(EditorMethods.ShowMessage, parameters);
        }

        /// <summary>
        /// Shows a message in the VS Code UI and awaits an action by the user.
        /// </summary>
        /// <param name="type">The type of message to show.</param>
        /// <param name="message">The message text.</param>
        /// <param name="actionItems">A list of actions to present to the user.</param>
        /// <returns></returns>
        public async Task<MessageActionItem> ShowMessageAsync(MessageType type, string message, params MessageActionItem[] actionItems)
        {
            ShowMessageRequestParams parameters = new ShowMessageRequestParams
            {
                Actions = actionItems,
                Message = message,
                Type = type
            };

            ResponseMessage response = await _server.SendRequestAsync(EditorMethods.ShowMessageRequest, parameters);

            return response.Result.ToObject<MessageActionItem>();
        }

        private void _server_NotificationReceived(object sender, NotificationMessage e)
        {
            throw new NotImplementedException();
        }

        private void _HandleNotification(object sender, NotificationMessage e)
        {
            if (e.Method.Equals(EditorMethods.DidChangeConfiguration))
            {
                ConfigurationChanged?.Invoke(this, e.Params.ToObject<DidChangeConfigurationParams>());
            }

            else if (e.Method.Equals(EditorMethods.DidChangeTextDocument))
            {
                TextDocumentChanged?.Invoke(this, e.Params.ToObject<DidChangeTextDocumentParams>());
            }

            else if (e.Method.Equals(EditorMethods.DidChangeWatchedFiles))
            {
                WatchedFilesChanged?.Invoke(this, e.Params.ToObject<DidChangeWatchedFilesParams>());
            }

            else if (e.Method.Equals(EditorMethods.DidCloseTextDocument))
            {
                TextDocumentClosed?.Invoke(this, e.Params.ToObject<DidCloseTextDocumentParams>());
            }

            else if (e.Method.Equals(EditorMethods.DidOpenTextDocument))
            {
                TextDocumentOpened?.Invoke(this, e.Params.ToObject<DidOpenTextDocumentParams>());
            }

            else if (e.Method.Equals(EditorMethods.DidSaveTextDocument))
            {
                TextDocumentSaved?.Invoke(this, e.Params.ToObject<DidSaveTextDocumentParams>());
            }
        }
    }
}
