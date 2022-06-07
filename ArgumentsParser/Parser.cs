using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ArgumentsParser
{
    /// <summary>
    /// Parser di riga di comando.
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// Stato del parser per la condivisione di informazioni tra handlers.
        /// </summary>
        public IParserStatus? Status { get; set; }
        /// <summary>
        /// Pattern utilizzato per l'analisi del comando.
        /// </summary>
        /// <see cref="Regex"/>
        public string ArgumentPattern { get; set; } = @"-(?<command>\w+)(\s(?<param>[\w\d,;]+)?)?";
        /// <summary>
        /// Evento lanciato prima dell'esecuzione di un comando.
        /// </summary>
        public event EventHandler<CommandExecutingEventArgs>? CommandExecuting;
        /// <summary>
        /// Evento lanciato dopo l'esecuzione di un comando.
        /// </summary>
        public event EventHandler<CommandExecutedEventArgs>? CommandExecuted;
        /// <summary>
        /// Logger del parser.
        /// </summary>
        private readonly ILogger? _logger;
        /// <summary>
        /// La catena dei comandi gestiti.
        /// </summary>
        private LinkedList<CommandHandler> Commands { get; set; } = new LinkedList<CommandHandler>();
        /// <summary>
        /// Costruttore.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public Parser(ILogger? logger = null) { _logger = logger; }
        /// <summary>
        /// Aggiunge un gestore di comando alla catena.
        /// </summary>
        /// <param name="commandHandler">Gestore del comando.</param>
        public Parser AddCommand(CommandHandler commandHandler) { Commands.AddLast(commandHandler); return this; }
        /// <summary>
        /// Rimuove un gestore di comando dalla catena.
        /// </summary>
        /// <param name="commandHandler">Gestore da rimuovere.</param>
        public Parser RemoveCommand(CommandHandler commandHandler) { Commands.Remove(commandHandler); return this; }
        /// <summary>
        /// Analizza la riga di comando e la gestisce.
        /// </summary>
        /// <param name="commandLine">Riga di comando.</param>
        public void ParseCommandLine(string commandLine)
        {
            var re = new Regex(ArgumentPattern, RegexOptions.Compiled);
            foreach (Match m in re.Matches(commandLine))
                Execute(this, m.Groups["command"].Value, m.Groups["param"].Value);
        }

        /// <summary>
        /// Esegue un comando.
        /// </summary>
        /// <param name="command">Comando da eseguire.</param>
        /// <param name="args">Parametri del comando.</param>
        protected virtual void Execute(Parser parser, string command, string args)
        {
            var c = Commands.First;
            if (c == null) return;

            _logger?.LogTrace($"Executing {command} with {args}");
            do
            {
                var e = new CommandExecutingEventArgs { Arguments = args, CommandName = command };
                CommandExecuting?.Invoke(this, e);
                if (e.Cancel)
                    c = null;
                else
                {
                    c!.Value.Execute(command, args, Status);
                    CommandExecuted?.Invoke(this, new CommandExecutedEventArgs { Arguments = args, CommandName = command });
                    c = c?.Next;
                }
            } while (!c?.Value.Handled ?? false);
            _logger?.LogTrace($"Executed {command} with {args}");
        }
    }
}
