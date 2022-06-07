using ArgumentsParser;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});
var logger = loggerFactory.CreateLogger<Program>();
var p = new Parser(logger);
p.AddCommand(new LogCommandHandler(logger));
p.CommandExecuting += (s,e) =>Console.WriteLine($"CommandExecuting for command {e.CommandName} with arguments: {e.Arguments}");
p.CommandExecuted += (s,e) =>Console.WriteLine($"CommandExecuted for command {e.CommandName} with arguments: {e.Arguments}");

p.ParseCommandLine("-command1 arg1,arg2,arg3 -command2 prova arg1 -command3");
