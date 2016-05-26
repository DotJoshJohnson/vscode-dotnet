using VSCode.Editor;

namespace VSCode
{
    public partial class LanguageServer
    {
        public EditorFeature Editor { get { return GetFeature<EditorFeature>(); } }
    }
}
