namespace VSCode.CodeLens
{
    /// <summary>
    /// Parameters provided to the <see cref="CodeLensFeature.DefineCodeLenses" /> event.
    /// </summary>
    public class CodeLensParams
    {
        /// <summary>
        /// The document VS Code is requesting CodeLenses for.
        /// </summary>
        public TextDocumentIdentifier TextDocument { get; set; }
    }
}
