namespace VSCode.Editor
{
    /// <summary>
    /// Methods defined by the VS Code Language Server Protocol.
    /// </summary>
    public static class EditorMethods
    {
        /// <summary>
        /// A notification sent from the client to the server to signal the change of configuration settings.
        /// </summary>
        public const string DidChangeConfiguration = "workspace/didChangeConfiguration";

        /// <summary>
        /// A notification sent from the client to the server to signal changes to a text document.
        /// </summary>
        public const string DidChangeTextDocument = "textDocument/didChange";

        /// <summary>
        /// A notification sent from the client to the server when the client detects changes to file watched by the lanaguage client.
        /// </summary>
        public const string DidChangeWatchedFiles = "textDocument/didChangeWatchedFiles";

        /// <summary>
        /// A notification sent from the client to the server when a document is closed in the client.
        /// </summary>
        public const string DidCloseTextDocument = "textDocument/didClose";

        /// <summary>
        /// A notification sent from the client to the server to signal newly opened text documents.
        /// </summary>
        public const string DidOpenTextDocument = "textDocument/didOpen";

        /// <summary>
        /// A notification sent from the client to the server when the document is saved in the client.
        /// </summary>
        public const string DidSaveTextDocument = "textDocument/didSave";

        /// <summary>
        /// A notification sent from the server to the client to ask the client to log a particular message.
        /// </summary>
        public const string LogMessage = "window/logMessage";

        /// <summary>
        /// A notification sent from the server to the client to ask the client to display a particular message in the user interface.
        /// </summary>
        public const string ShowMessage = "window/showMessage";

        /// <summary>
        /// A request sent from the server to the client to ask the client to display a particular message in the user interface.
        /// </summary>
        public const string ShowMessageRequest = "window/showMessageRequest";
    }
}
