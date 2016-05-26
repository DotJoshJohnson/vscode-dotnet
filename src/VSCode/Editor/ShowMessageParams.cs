namespace VSCode.Editor
{
    /// <summary>
    /// Parameters provided to the <see cref="EditorMethods.ShowMessage" /> method.
    /// </summary>
    public class ShowMessageParams
    {
        /// <summary>
        /// The message to display in the VS Code UI.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The type of message to display.
        /// </summary>
        public MessageType Type { get; set; }
    }
}
