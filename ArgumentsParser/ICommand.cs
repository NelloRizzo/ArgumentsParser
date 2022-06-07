using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentsParser
{
    public interface ICommand
    {
        public bool Handled { get; set; }
        public void Execute(string command, string args);
    }
}
