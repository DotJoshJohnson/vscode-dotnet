namespace VSCode.Formatting
{
    /// <summary>
    /// Parameters provided to the <see cref="FormattingFeature.FormatDocumentRange" /> event.
    /// </summary>
    public class DocumentRangeFormattingParams
    {
        /// <summary>
        /// Describes the options that should be used by the implemented formatter.
        /// </summary>
        public FormattingOptions Options { get; set; }

        /// <summary>
        /// The range to be formatted.
        /// </summary>
        public Range Range { get; set; }

        /// <summary>
        /// The text document where formatting should occur.
        /// </summary>
        public TextDocumentIdentifier TextDocument { get; set; }
    }
}
