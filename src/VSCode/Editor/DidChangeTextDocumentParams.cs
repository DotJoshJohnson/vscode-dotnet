using System.Collections.Generic;

namespace VSCode.Editor
{
    /// <summary>
    /// Parameters provided to the <see cref="EditorFeature.TextDocumentChanged" /> event.
    /// </summary>
    public class DidChangeTextDocumentParams
    {
        /// <summary>
        /// A collection of <see cref="TextDocumentContentChangeEvent" /> instances representing changes to the document.
        /// </summary>
        public IEnumerable<TextDocumentContentChangeEvent> ContentChanges { get; set; }

        /// <summary>
        /// The document to which changes were made.
        /// </summary>
        public VersionedTextDocumentIdentifier TextDocument { get; set; }
    }
}
