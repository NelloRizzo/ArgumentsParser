using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentsParser
{
    /// <summary>
    /// Un processore di comando che esegue solo il log del comando.
    /// </summary>
    public class LogCommandHandler : CommandHandler
    {
        /// <summary>
        /// Il logger per l'output.
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// Costruttore.
        /// </summary>
        /// <param name="logger">Il logger per l'output.</param>
        public LogCommandHandler(ILogger logger) { _logger = logger; }
        /// <summary>
        /// Esegue il log del comando.
        /// </summary>
        public override void Execute(string command, string args) =>
            _logger.LogInformation($"Log for command <{command}> with arguments: ({args})");
    }
}
