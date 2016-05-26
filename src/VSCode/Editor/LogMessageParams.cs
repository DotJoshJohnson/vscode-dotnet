namespace VSCode.Editor
{
    /// <summary>
    /// Parameters provided to the <see cref="EditorMethods.LogMessage" /> notification.
    /// </summary>
    public class LogMessageParams
    {
        /// <summary>
        /// The message text.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The type of message to log.
        /// </summary>
        public MessageType Type { get; set; }
    }
}
