using VSCode.CodeLens;
using VSCode.Formatting;

namespace VSCode
{
    /// <summary>
    /// The capabilities the language server provides.
    /// </summary>
    public class ServerCapabilities
    {
        /// <summary>
        /// Creates a new <see cref="ServerCapabilities" /> instance.
        /// </summary>
        public ServerCapabilities()
        {
            TextDocumentSync = TextDocumentSyncKind.Incremental;
        }

        /// <summary>
        /// When not <c>null</c>, the server is known to provide CodeLens support.
        /// </summary>
        public CodeLensOptions CodeLensProvider { get; set; }

        /// <summary>
        /// When <c>true</c>, the server is known to support document formatting.
        /// </summary>
        public bool DocumentFormattingProvider { get; set; }

        /// <summary>
        /// When not <c>null</c>, the server is known to support "on-the-fly" formatting.
        /// </summary>
        public DocumentOnTypeFormattingOptions DocumentOnTypeFormattingProvider { get; set; }

        /// <summary>
        /// When <c>true</c>, the server is known to support range formatting within documents.
        /// </summary>
        public bool DocumentRangeFormattingProvider { get; set; }

        /// <summary>
        /// When <c>true</c>, the server is known to provide Hover support.
        /// </summary>
        public bool HoverProvider { get; set; }

        /// <summary>
        /// The method for document synchronization. This defaults to <see cref="TextDocumentSyncKind.Incremental" /> (recommended).
        /// </summary>
        public TextDocumentSyncKind TextDocumentSync { get; set; }
    }
}
