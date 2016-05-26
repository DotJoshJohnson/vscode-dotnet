namespace VSCode.Editor
{
    /// <summary>
    /// Represents an incremental change to a text document.
    /// </summary>
    public class TextDocumentContentChangeEvent
    {
        /// <summary>
        /// The <see cref="Range" /> to which this change applies.
        /// </summary>
        public Range Range { get; set; }

        /// <summary>
        /// The total length of the range.
        /// </summary>
        public int RangeLength { get; set; }

        /// <summary>
        /// The new text applied to the specified range.
        /// </summary>
        public string Text { get; set; }
    }
}
