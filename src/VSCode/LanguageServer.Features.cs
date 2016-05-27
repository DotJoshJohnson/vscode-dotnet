using VSCode.Editor;
using VSCode.Formatting;

namespace VSCode
{
    public partial class LanguageServer
    {
        public EditorFeature Editor { get { return GetFeature<EditorFeature>(); } }
        public FormattingFeature Formatting { get { return GetFeature<FormattingFeature>(); } }
    }
}
