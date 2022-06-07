using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ArgumentsParser
{
    public class Parser
    {
        public string ArgumentPattern { get; set; } = @"-(?<command>\w+)(\s(?<param>[\w\d,;]+)?)?";

        public event EventHandler<CommandExecutingEventArgs>? CommandExecuting;
        public event EventHandler<CommandExecutedEventArgs>? CommandExecuted;

        private readonly ILogger? _logger;
        private LinkedList<Command> Commands { get; set; } = new LinkedList<Command>();

        public Parser(ILogger? logger = null) { _logger = logger; }

        public void AddCommand(Command command) { Commands.AddLast(command); }
        public void RemoveCommand(Command command) { Commands.Remove(command); }

        public void ParseCommandLine(string commandLine)
        {
            var re = new Regex(ArgumentPattern, RegexOptions.Compiled);
            foreach (Match m in re.Matches(commandLine))
                Execute(m.Groups["command"].Value, m.Groups["param"].Value);
        }

        protected virtual void Execute(string command, string args)
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
                    c!.Value.Execute(command, args);
                    CommandExecuted?.Invoke(this, new CommandExecutedEventArgs { Arguments = args, CommandName = command });
                    c = c?.Next;
                }
            } while (!c?.Value.Handled ?? false);
            _logger?.LogTrace($"Executed {command} with {args}");
        }
    }
}
