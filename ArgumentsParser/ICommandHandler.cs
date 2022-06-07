using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentsParser
{
    /// <summary>
    /// Definizione di un comando.
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>
        /// Indica se la gestione è terminata.
        /// </summary>
        /// <remarks>Interrompe l'esecuzione della catena dei comandi se impostato a <em>true</em>.</remarks>
        public bool Handled { get; set; }
        /// <summary>
        /// Esegue un comando.
        /// </summary>
        /// <param name="command">Il comando da eseguire.</param>
        /// <param name="args">Gli argomenti passati al comando.</param>
        /// <param name="status">Lo stato del parser per la condivisione di informazioni tra handlers.</param>
        public void Execute(string command, string args, IParserStatus? status);
    }
}
