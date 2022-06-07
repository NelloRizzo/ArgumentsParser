using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentsParser
{
    /// <summary>
    /// Argomenti per l'evento lanciato prima dell'esecuzione del comando.
    /// </summary>
    public class CommandExecutingEventArgs : EventArgs
    {
        /// <summary>
        /// Indica se annullare l'esecuzione.
        /// </summary>
        public bool Cancel { get; set; }
        /// <summary>
        /// Comando che sta per essere eseguito.
        /// </summary>
        public string? CommandName { get; set; }
        /// <summary>
        /// Argomenti del comando.
        /// </summary>
        public string? Arguments { get; set; }
    }
}
