namespace VSCode.Formatting
{
    /// <summary>
    /// Parameters provided to the <see cref="FormattingFeature.FormatDocumentOnType" /> event.
    /// </summary>
    public class DocumentOnTypeFormattingParams
    {
        /// <summary>
        /// The character that has been typed.
        /// </summary>
        public string Ch { get; set; }

        /// <summary>
        /// Describes the options that should be used by the implemented formatter.
        /// </summary>
        public FormattingOptions Options { get; set; }

        /// <summary>
        /// The position at which the request was sent.
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// The text document to format.
        /// </summary>
        public TextDocumentIdentifier TextDocument { get; set; }
    }
}
