using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentsParser
{
    /// <summary>
    /// Argomenti per l'evento lanciato prima dell'esecuzione del comando.
    /// </summary>
    public class CommandExecutingEventArgs : CommandParserEventArgs
    {
        /// <summary>
        /// Indica se annullare l'esecuzione.
        /// </summary>
        public bool Cancel { get; set; }
    }
}
