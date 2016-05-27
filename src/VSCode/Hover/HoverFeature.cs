using System;

namespace VSCode.Hover
{
    public class HoverFeature : IFeature
    {
        private LanguageServer _server;

        public event EventHandler<RequestContext<TextDocumentPositionParams, Hover>> Hover;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialize(LanguageServer languageServer)
        {
            _server = languageServer;

            _server.RequestReceived += _HandleRequest;
        }

        private void _HandleRequest(object sender, RequestContext e)
        {
            if (e.Request.Method.Equals(HoverMethods.Hover))
            {
                Hover?.Invoke(this, new RequestContext<TextDocumentPositionParams, Hover>(e));
            }
        }
    }
}
