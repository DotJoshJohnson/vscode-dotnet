using System;
using System.Collections.Generic;

namespace VSCode.Formatting
{
    /// <summary>
    /// Represents formatting features in VS Code.
    /// </summary>
    public class FormattingFeature : IFeature
    {
        private LanguageServer _server;

        /// <summary>
        /// Raised when a full document is to be formatted.
        /// </summary>
        public event EventHandler<RequestContext<DocumentFormattingParams, IEnumerable<TextEdit>>> FormatDocument;

        /// <summary>
        /// Raised when a document should be formatted "on-the-fly" while the user is typing.
        /// </summary>
        public event EventHandler<RequestContext<DocumentOnTypeFormattingParams, IEnumerable<TextEdit>>> FormatDocumentOnType;

        /// <summary>
        /// Raised when a specific range within a document is to be formatted.
        /// </summary>
        public event EventHandler<RequestContext<DocumentRangeFormattingParams, IEnumerable<TextEdit>>> FormatDocumentRange;

        /// <summary>
        /// See <see cref="IDisposable.Dispose" />.
        /// </summary>
        public void Dispose()
        {
            _server.RequestReceived -= _HandleRequest;
        }

        /// <summary>
        /// See <see cref="IFeature.Initialize(LanguageServer)" />. Should only be called by the language server.
        /// </summary>
        /// <param name="languageServer">The calling language server.</param>
        public void Initialize(LanguageServer languageServer)
        {
            _server = languageServer;

            _server.RequestReceived += _HandleRequest;
        }

        private void _HandleRequest(object sender, RequestContext e)
        {
            if (e.Request.Method.Equals(FormattingMethods.Formatting))
            {
                FormatDocument?.Invoke(this, new RequestContext<DocumentFormattingParams, IEnumerable<TextEdit>>(e));
            }

            else if (e.Request.Method.Equals(FormattingMethods.OnTypeFormatting))
            {
                FormatDocumentOnType?.Invoke(this, new RequestContext<DocumentOnTypeFormattingParams, IEnumerable<TextEdit>>(e));
            }

            else if (e.Request.Method.Equals(FormattingMethods.RangeFormatting))
            {
                FormatDocumentRange?.Invoke(this, new RequestContext<DocumentRangeFormattingParams, IEnumerable<TextEdit>>(e));
            }
        }
    }
}
