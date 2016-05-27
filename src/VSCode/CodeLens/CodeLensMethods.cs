namespace VSCode.CodeLens
{
    /// <summary>
    /// Methods defined by the VS Code Language Server Protocol.
    /// </summary>
    public static class CodeLensMethods
    {
        /// <summary>
        /// Sent when VS Code needs CodeLens definitions for a document.
        /// </summary>
        public const string CodeLens = "textDocument/codeLens";

        /// <summary>
        /// Sent when VS Code needs to resolve a CodeLens.
        /// </summary>
        public const string Resolve = "codeLens/resolve";
    }
}
