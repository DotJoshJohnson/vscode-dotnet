namespace VSCode.Editor
{
    /// <summary>
    /// A type given to a <see cref="FileEvent" /> to denote the type of change made to a file.
    /// </summary>
    public enum FileChangeType
    {
        /// <summary>
        /// The file was created.
        /// </summary>
        Created = 1,

        /// <summary>
        /// The file was changed.
        /// </summary>
        Changed = 2,

        /// <summary>
        /// The file was deleted.
        /// </summary>
        Deleted = 3
    }
}
