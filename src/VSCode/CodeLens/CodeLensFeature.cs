using System;
using System.Collections.Generic;

namespace VSCode.CodeLens
{
    /// <summary>
    /// Represents features related to the CodeLens functionality in VS Code.
    /// </summary>
    public class CodeLensFeature : IFeature
    {
        private LanguageServer _server;

        /// <summary>
        /// Raised when VS Code requests CodeLens items for a text document.
        /// </summary>
        public event EventHandler<RequestContext<CodeLensParams, IEnumerable<CodeLens>>> DefineCodeLenses;

        /// <summary>
        /// Raised when VS Code is ready to display a CodeLens in the UI.
        /// </summary>
        public event EventHandler<RequestContext<CodeLens, CodeLens>> ResolveCodeLens;

        /// <summary>
        /// See <see cref="IDisposable.Dispose" />.
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="IFeature.Initialize(LanguageServer)" />. This method should only be called by the language server.
        /// </summary>
        /// <param name="languageServer">The calling language server instance.</param>
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
