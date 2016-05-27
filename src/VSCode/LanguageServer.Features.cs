using VSCode.CodeLens;
using VSCode.Editor;
using VSCode.Formatting;
using VSCode.Hover;

namespace VSCode
{
    public partial class LanguageServer
    {
        public CodeLensFeature CodeLens { get { return GetFeature<CodeLensFeature>(); } }
        public EditorFeature Editor { get { return GetFeature<EditorFeature>(); } }
        public FormattingFeature Formatting { get { return GetFeature<FormattingFeature>(); } }
        public HoverFeature TextHover { get { return GetFeature<HoverFeature>(); } }
    }
}
