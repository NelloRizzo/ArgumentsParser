using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentsParser
{
    public abstract class Command : ICommand
    {
        public bool Handled { get; set; }
        public abstract void Execute(string command, string args);
    }
}
