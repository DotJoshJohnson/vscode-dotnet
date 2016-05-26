using Newtonsoft.Json.Linq;

namespace VSCode.Editor
{
    /// <summary>
    /// Parameters provided to the <see cref="EditorFeature.ConfigurationChanged" /> event.
    /// </summary>
    public class DidChangeConfigurationParams
    {
        /// <summary>
        /// A <see cref="JObject" /> representing the settings from VS Code.
        /// </summary>
        public JObject Settings { get; set; }
    }
}
