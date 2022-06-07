using ArgumentsParser;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});
var logger = loggerFactory.CreateLogger<Program>();
var p = new Parser(logger) { Status = new CounterStatus() };
p.AddCommand(new LogCommandHandler(logger)).AddCommand(new CountCommandHandler());
p.CommandExecuting += (s, e) => Console.WriteLine($"CommandExecuting for command {e.CommandName} with arguments: {e.Arguments}");
p.CommandExecuted += (s, e) => Console.WriteLine($"CommandExecuted for command {e.CommandName} with arguments: {e.Arguments}");

p.ParseCommandLine("-command1 arg1,arg2,arg3 -command2 prova arg1 -command3");
Console.WriteLine($"Lo stato del parser è {p.Status}");

var r = p.ParseCommandLineAsync("-command1 arg1,arg2,arg3 -command2 prova arg1 -command3");
Console.WriteLine("Fine");
while (r.Status != TaskStatus.RanToCompletion) ;
Console.WriteLine("Adesso ho terminato davvero");

class CounterStatus : IParserStatus
{
    public int Value { get; set; } = 0;
    public override string ToString() => $"Value = {Value}";
}

class CountCommandHandler : CommandHandler
{
    public override void Execute(string command, string args, IParserStatus? status) { if (status is CounterStatus s) s.Value++; }
}