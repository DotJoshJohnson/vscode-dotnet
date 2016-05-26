namespace VSCode.Editor
{
    /// <summary>
    /// Parameters provided to the <see cref="EditorFeature.TextDocumentOpened" /> event.
    /// </summary>
    public class DidOpenTextDocumentParams
    {
        /// <summary>
        /// The document that was opened.
        /// </summary>
        public TextDocumentItem TextDocument { get; set; }
    }
}
