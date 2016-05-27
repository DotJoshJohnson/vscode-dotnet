namespace VSCode.Formatting
{
    public class DocumentRangeFormattingParams
    {
        public FormattingOptions Options { get; set; }
        public Range Range { get; set; }
        public TextDocumentIdentifier TextDocument { get; set; }
    }
}
