namespace VSCode.Editor
{
    /// <summary>
    /// Parameters provided to the <see cref="EditorFeature.TextDocumentClosed" /> event.
    /// </summary>
    public class DidCloseTextDocumentParams
    {
        /// <summary>
        /// The document that was closed.
        /// </summary>
        public TextDocumentIdentifier TextDocument { get; set; }
    }
}
