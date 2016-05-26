namespace VSCode
{
    public class TextDocumentItem
    {
        public string LanguageId { get; set; }
        public string Text { get; set; }
        public string Uri { get; set; }
        public int Version { get; set; }
    }
}
