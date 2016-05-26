using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSCode
{
    public class InitializeResult
    {
        /// <summary>
        /// The capabilities the language server provides.
        /// </summary>
        public ServerCapabilities Capabilities { get; set; }
    }
}
