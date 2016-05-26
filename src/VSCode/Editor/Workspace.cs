using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VSCode.Editor
{
    /// <summary>
    /// A virtual workspace representing the actual workspace in VS Code.
    /// </summary>
    public class Workspace : IDisposable
    {
        private List<WorkspaceDocument> _documents;
        private EditorFeature _editor;

        /// <summary>
        /// creates a new <see cref="Workspace" /> instance using the provided <see cref="EditorFeature" />.
        /// </summary>
        /// <param name="editor"></param>
        public Workspace(EditorFeature editor)
        {
            _documents = new List<WorkspaceDocument>();
            _editor = editor;

            _SubscribeEvents();

            State = new Dictionary<string, object>();
        }

        /// <summary>
        /// Raised when a <see cref="WorkspaceDocument" /> has changed.
        /// </summary>
        public event EventHandler<WorkspaceDocument> DocumentChanged;

        /// <summary>
        /// Raised when a <see cref="WorkspaceDocument" /> has closed.
        /// </summary>
        public event EventHandler<WorkspaceDocument> DocumentClosed;

        /// <summary>
        /// Raised when a <see cref="WorkspaceDocument" /> has opened.
        /// </summary>
        public event EventHandler<WorkspaceDocument> DocumentOpened;

        /// <summary>
        /// A read-only list of <see cref="WorkspaceDocument" /> instances.
        /// </summary>
        public IReadOnlyList<WorkspaceDocument> Documents
        {
            get
            {
                return new ReadOnlyCollection<WorkspaceDocument>(_documents);
            }
        }

        /// <summary>
        /// A state container that can be used to store objects within the context of the workspace.
        /// </summary>
        public Dictionary<string, object> State { get; private set; }

        /// <summary>
        /// See <see cref="IDisposable.Dispose" />.
        /// </summary>
        public void Dispose()
        {
            _UnsubscribeEvents();
        }

        private void _HandleTextDocumentChanged(object sender, DidChangeTextDocumentParams e)
        {
            WorkspaceDocument document = _documents.Where(x => x.Uri.Equals(e.TextDocument.Uri)).FirstOrDefault();

            if (document != null)
            {
                document.ApplyContentChanges(e.ContentChanges, e.TextDocument.Version);
                DocumentChanged?.Invoke(this, document);
            }
        }

        private void _HandleTextDocumentClosed(object sender, DidCloseTextDocumentParams e)
        {
            WorkspaceDocument document = _documents.Where(x => x.Uri.Equals(e.TextDocument.Uri)).FirstOrDefault();

            if (document != null)
            {
                _documents.Remove(document);
                DocumentClosed?.Invoke(this, document);
            }
        }

        private void _HandleTextDocumentOpened(object sender, DidOpenTextDocumentParams e)
        {
            WorkspaceDocument document = new WorkspaceDocument(e.TextDocument.LanguageId, e.TextDocument.Text, e.TextDocument.Uri, e.TextDocument.Version);
            _documents.Add(document);

            DocumentOpened?.Invoke(this, document);
        }

        private void _SubscribeEvents()
        {
            _editor.TextDocumentChanged += _HandleTextDocumentChanged;
            _editor.TextDocumentClosed += _HandleTextDocumentClosed;
            _editor.TextDocumentOpened += _HandleTextDocumentOpened;
        }

        private void _UnsubscribeEvents()
        {
            _editor.TextDocumentChanged -= _HandleTextDocumentChanged;
            _editor.TextDocumentClosed -= _HandleTextDocumentClosed;
            _editor.TextDocumentOpened -= _HandleTextDocumentOpened;
        }
    }
}
