using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentsParser
{
    /// <summary>
    /// Argomenti dell'evento lanciato dopo l'esecuzione del comando.
    /// </summary>
    public class CommandExecutedEventArgs : EventArgs
    {
        /// <summary>
        /// Nome del comando eseguito.
        /// </summary>
        public string? CommandName { get; set; }
        /// <summary>
        /// Argomenti del comando.
        /// </summary>
        public string? Arguments { get; set; }
    }
}
