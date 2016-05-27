namespace VSCode.Formatting
{
    /// <summary>
    /// Parameters provided to the <see cref="FormattingFeature.FormatDocument" /> event.
    /// </summary>
    public class DocumentFormattingParams
    {
        /// <summary>
        /// Describes the options that should be used by the implemented formatter.
        /// </summary>
        public FormattingOptions Options { get; set; }

        /// <summary>
        /// The text document to format.
        /// </summary>
        public TextDocumentIdentifier TextDocument { get; set; }
    }
}
