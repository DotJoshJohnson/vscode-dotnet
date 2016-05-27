namespace VSCode.CodeLens
{
    /// <summary>
    /// Represents an <see cref="EditorCommand" /> that should be shown along with source text, like the number of references, a way to run tests, etc.
    /// </summary>
    public class CodeLens
    {
        /// <summary>
        /// The <see cref="EditorCommand" /> this <see cref="CodeLens" /> represents.
        /// </summary>
        public EditorCommand Command { get; set; }

        /// <summary>
        /// A data entry field that is preserved on a code lens item between the definition and resolution requests.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// The range to which this CodeLens applies. Should only span a single line.
        /// </summary>
        public Range Range { get; set; }
    }
}
