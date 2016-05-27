namespace VSCode.CodeLens
{
    /// <summary>
    /// Options passed to VS Code to define the server's CodeLens capabilities.
    /// </summary>
    public class CodeLensOptions
    {
        /// <summary>
        /// When <c>true</c>, VS Code will send CodeLens resolution requests.
        /// </summary>
        public bool ResolveProvider { get; set; }
    }
}
