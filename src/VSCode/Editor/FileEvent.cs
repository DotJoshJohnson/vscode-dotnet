namespace VSCode.Editor
{
    /// <summary>
    /// An event describing a change to a file.
    /// </summary>
    public class FileEvent
    {
        /// <summary>
        /// The type of event/change made.
        /// </summary>
        public FileChangeType Type { get; set; }

        /// <summary>
        /// The URI of the changed file.
        /// </summary>
        public string Uri { get; set; }
    }
}
