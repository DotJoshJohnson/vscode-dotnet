namespace VSCode.Formatting
{
    public class DocumentOnTypeFormattingParams
    {
        public string Ch { get; set; }
        public FormattingOptions Options { get; set; }
        public Position Position { get; set; }
        public TextDocumentIdentifier TextDocument { get; set; }
    }
}
