namespace VSCode.Formatting
{
    public class DocumentFormattingParams
    {
        public FormattingOptions Options { get; set; }
        public TextDocumentIdentifier TextDocument { get; set; }
    }
}
