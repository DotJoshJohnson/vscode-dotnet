using System;
using System.Text;
using System.Collections.Generic;

namespace VSCode.Editor
{
    /// <summary>
    /// A virtual document representing a TextDocument in VS Code.
    /// </summary>
    public class WorkspaceDocument
    {
        /// <summary>
        /// Creates a new <see cref="WorkspaceDocument" /> instance using the provided language, text, uri, and version.
        /// </summary>
        /// <param name="languageId">The VS Code language ID to assigned to this document.</param>
        /// <param name="text">The initial text of this document.</param>
        /// <param name="uri">The VS Code URI of this document.</param>
        /// <param name="version">The initial version of this document.</param>
        public WorkspaceDocument(string languageId, string text, string uri, int version)
        {
            LanguageId = languageId;
            Text = text;
            Uri = uri;
            Version = version;
        }

        /// <summary>
        /// The VS Code language ID assigned to the document.
        /// </summary>
        public string LanguageId { get; private set; }

        /// <summary>
        /// A state container that can be used to store objects within the context of the document.
        /// </summary>
        public Dictionary<string, object> State { get; private set; }

        /// <summary>
        /// The text of the document for the current <see cref="Version" />.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// The VS Code URI for the document.
        /// </summary>
        public string Uri { get; private set; }

        /// <summary>
        /// The current version of the document.
        /// This may differ from the version present in VS Code if the last change notification has not been sent or processed.
        /// </summary>
        public int Version { get; private set; }

        internal void ApplyContentChanges(IEnumerable<TextDocumentContentChangeEvent> changes, int newVersion)
        {
            StringBuilder builder = new StringBuilder(Text);
            
            foreach (TextDocumentContentChangeEvent change in changes)
            {
                int index = _GetRangeStartIndex(change.Range);

                if ((index + change.RangeLength) <= builder.Length)
                {
                    builder.Remove(index, change.RangeLength);
                }

                else
                {
                    builder.Remove(index, (builder.Length - index));
                }

                builder.Insert(index, change.Text);
            }

            Text = builder.ToString();
            Version = newVersion;
        }

        private int _GetRangeStartIndex(Range range)
        {
            string lineEnding = Text.Contains("\r\n") ? "\r\n" : "\n";
            string[] lines = Text.Split(new string[] { lineEnding }, StringSplitOptions.None);

            int index = range.Start.Character;

            for (int i = 0; i < range.Start.Line; i++)
            {
                index += (lines[i].Length + lineEnding.Length);
            }

            return index;
        }
    }
}
