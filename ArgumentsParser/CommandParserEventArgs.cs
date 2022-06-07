using System;

namespace ArgumentsParser
{
    public class CommandParserEventArgs : EventArgs
    {
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