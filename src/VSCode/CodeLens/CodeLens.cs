namespace VSCode.CodeLens
{
    public class CodeLens
    {
        public EditorCommand Command { get; set; }
        public object Data { get; set; }
        public Range Range { get; set; }
    }
}
