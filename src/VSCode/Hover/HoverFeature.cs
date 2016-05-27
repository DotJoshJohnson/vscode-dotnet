using System;

namespace VSCode.Hover
{
    /// <summary>
    /// Represents the hover feature in VS Code.
    /// </summary>
    public class HoverFeature : IFeature
    {
        private LanguageServer _server;

        /// <summary>
        /// Raised when VS Code needs a hover for a given position in a text document.
        /// </summary>
        public event EventHandler<RequestContext<TextDocumentPositionParams, Hover>> Hover;

        /// <summary>
        /// See <see cref="IDisposable.Dispose" />.
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="IFeature.Initialize(LanguageServer)" />. Should only be called by the language server.
        /// </summary>
        /// <param name="languageServer">Te calling lanuage server.</param>
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
