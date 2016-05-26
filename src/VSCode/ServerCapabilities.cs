namespace VSCode
{
    /// <summary>
    /// The capabilities the language server provides.
    /// </summary>
    public class ServerCapabilities
    {
        public ServerCapabilities()
        {
            TextDocumentSync = TextDocumentSyncKind.Incremental;
        }

        public TextDocumentSyncKind TextDocumentSync { get; set; }
    }
}
