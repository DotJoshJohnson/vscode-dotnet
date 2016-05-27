namespace VSCode.Formatting
{
    /// <summary>
    /// Options to be used by formatters when formatting text documents.
    /// </summary>
    public class FormattingOptions
    {
        /// <summary>
        /// When <c>true</c>, tabs should be converted to spaces in the text document.
        /// </summary>
        public bool InsertSpaces { get; set; }

        /// <summary>
        /// The number of spaces that are equal to a single tab.
        /// </summary>
        public int TabSize { get; set; }
    }
}
