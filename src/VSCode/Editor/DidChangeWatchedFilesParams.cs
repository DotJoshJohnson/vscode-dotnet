using System.Collections.Generic;

namespace VSCode.Editor
{
    /// <summary>
    /// Parameters provided to the <see cref="EditorFeature.WatchedFilesChanged" /> event.
    /// </summary>
    public class DidChangeWatchedFilesParams
    {
        /// <summary>
        /// A collection of <see cref="FileEvent" /> instances representing changes made to watched files.
        /// </summary>
        public IEnumerable<FileEvent> Changes { get; set; }
    }
}
