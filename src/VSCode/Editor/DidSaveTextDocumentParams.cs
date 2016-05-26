namespace VSCode.Editor
{
    /// <summary>
    /// Parameters provided to the <see cref="EditorFeature.TextDocumentSaved" /> event.
    /// </summary>
    public class DidSaveTextDocumentParams
    {
        /// <summary>
        /// The document that was saved.
        /// </summary>
        public TextDocumentIdentifier TextDocument { get; set; }
    }
}
