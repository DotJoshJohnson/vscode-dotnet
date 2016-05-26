namespace VSCode
{
    public class InitializeParams
    {
        /// <summary>
        /// The capabilities provided by the client (editor).
        /// </summary>
        public ClientCapabilities Capabilities { get; set; }

        /// <summary>
        /// The process Id of the parent process that started the server.
        /// </summary>
        public int ProcessId { get; set; }

        /// <summary>
        /// The root path of the workspace. Is null if no folder is open.
        /// </summary>
        public string RootPath { get; set; }
    }
}
