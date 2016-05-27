namespace VSCode.Formatting
{
    /// <summary>
    /// Methods defined by the VS Code Language Server Protocol.
    /// </summary>
    public static class FormattingMethods
    {
        /// <summary>
        /// Sent when a full document is to be formatted.
        /// </summary>
        public const string Formatting = "textDocument/formatting";

        /// <summary>
        /// Sent when a document should be formatted "on-the-fly" while the user is typing.
        /// </summary>
        public const string OnTypeFormatting = "textDocument/onTypeFormatting";

        /// <summary>
        /// Sent when a specific range within a document is to be formatted.
        /// </summary>
        public const string RangeFormatting = "textDocument/rangeFormatting";
    }
}
