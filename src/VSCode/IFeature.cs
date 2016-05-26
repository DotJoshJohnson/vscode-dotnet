using System;

namespace VSCode
{
    public interface IFeature : IDisposable
    {
        void Initialize(LanguageServer languageServer);
    }
}
