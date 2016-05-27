using VSCode.CodeLens;
using VSCode.Editor;
using VSCode.Formatting;

namespace VSCode
{
    public partial class LanguageServer
    {
        public CodeLensFeature CodeLens { get { return GetFeature<CodeLensFeature>(); } }
        public EditorFeature Editor { get { return GetFeature<EditorFeature>(); } }
        public FormattingFeature Formatting { get { return GetFeature<FormattingFeature>(); } }
    }
}
