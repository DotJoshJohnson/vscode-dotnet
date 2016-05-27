using System;
using System.Collections.Generic;

namespace VSCode.CodeLens
{
    public class CodeLensFeature : IFeature
    {
        private LanguageServer _server;

        public event EventHandler<RequestContext<CodeLensParams, IEnumerable<CodeLens>>> DefineCodeLenses;
        public event EventHandler<RequestContext<CodeLens, CodeLens>> ResolveCodeLens;

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
            if (e.Request.Method.Equals(CodeLensMethods.CodeLens))
            {
                DefineCodeLenses?.Invoke(this, new RequestContext<CodeLensParams, IEnumerable<CodeLens>>(e));
            }

            else if (e.Request.Method.Equals(CodeLensMethods.Resolve))
            {
                ResolveCodeLens?.Invoke(this, new RequestContext<CodeLens, CodeLens>(e));
            }
        }
    }
}
