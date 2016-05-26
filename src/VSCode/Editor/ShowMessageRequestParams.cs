using System.Collections.Generic;

namespace VSCode.Editor
{
    /// <summary>
    /// Parameters provided to the <see cref="EditorMethods.ShowMessageRequest" /> method.
    /// </summary>
    public class ShowMessageRequestParams
    {
        /// <summary>
        /// A collection of <see cref="MessageActionItem" /> instances to offer to the user.
        /// </summary>
        public IEnumerable<MessageActionItem> Actions { get; set; }

        /// <summary>
        /// The message to display.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The type of message to display.
        /// </summary>
        public MessageType Type { get; set; }
    }
}
