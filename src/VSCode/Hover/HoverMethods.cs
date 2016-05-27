namespace VSCode.Hover
{
    /// <summary>
    /// Methods defined by the VS Code Language Server Protocol.
    /// </summary>
    public static class HoverMethods
    {
        /// <summary>
        /// Sent when VS Code needs a hover for a given position in a text document.
        /// </summary>
        public const string Hover = "textDocument/hover";
    }
}
