namespace VSCode.Editor
{
    /// <summary>
    /// Represents an action item for a UI message in VS Code.
    /// </summary>
    public class MessageActionItem
    {
        /// <summary>
        /// Creates a new <see cref="MessageActionItem" /> instance.
        /// </summary>
        public MessageActionItem() { }

        /// <summary>
        /// Creates a new <see cref="MessageActionItem" /> instance using the provided title.
        /// </summary>
        /// <param name="title"></param>
        public MessageActionItem(string title)
        {
            Title = title;
        }

        /// <summary>
        /// A short title like "Retry", "Open Log", etc.
        /// </summary>
        public string Title { get; set; }
    }
}
