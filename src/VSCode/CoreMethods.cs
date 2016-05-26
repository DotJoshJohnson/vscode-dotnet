namespace VSCode
{
    /// <summary>
    /// Methods defined by the VS Code Language Server Protocol.
    /// </summary>
    public static class CoreMethods
    {
        /// <summary>
        /// Sent when a request should be cancelled.
        /// </summary>
        public const string CancelRequest = "$/cancelRequest";

        /// <summary>
        /// Sent by VS Code to ask the server to exit.
        /// </summary>
        public const string Exit = "exit";

        /// <summary>
        /// Sent by VS Code when the server is being initialized.
        /// </summary>
        public const string Initialize = "initialize";

        /// <summary>
        /// Sent by VS Code to ask the server to shut down (VSCode.NET currently does not do anything except respond to the request).
        /// </summary>
        public const string Shutdown = "shutdown";
    }
}
