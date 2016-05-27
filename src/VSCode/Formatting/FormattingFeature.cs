using System;
using System.Collections.Generic;

namespace VSCode.Formatting
{
    public class FormattingFeature : IFeature
    {
        public LanguageServer _server;

        public event EventHandler<RequestContext<DocumentFormattingParams, IEnumerable<TextEdit>>> FormatDocument;
        public event EventHandler<RequestContext<DocumentOnTypeFormattingParams, IEnumerable<TextEdit>>> FormatDocumentOnType;
        public event EventHandler<RequestContext<DocumentRangeFormattingParams, IEnumerable<TextEdit>>> FormatDocumentRange;

        public void Dispose()
        {
            _server.RequestReceived -= _HandleRequest;
        }

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
