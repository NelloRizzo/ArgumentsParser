using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentsParser
{
    public class CommandLog : Command
    {
        private readonly ILogger _logger;
        public CommandLog(ILogger logger) { _logger = logger; }
        public override void Execute(string command, string args) =>
            _logger.LogInformation($"Log for command <{command}> with arguments: ({args})");
    }
}
