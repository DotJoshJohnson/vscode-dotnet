namespace VSCode.Formatting
{
    /// <summary>
    /// Format document on type options.
    /// </summary>
    public class DocumentOnTypeFormattingOptions
    {
        /// <summary>
        /// A character on which formatting should be triggered.
        /// </summary>
        public string FirstTriggerCharacter { get; set; }

        /// <summary>
        /// More trigger characters.
        /// </summary>
        public string MoreTriggerCharacter { get; set; }
    }
}
