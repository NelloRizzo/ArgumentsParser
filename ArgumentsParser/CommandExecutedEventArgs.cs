using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentsParser
{
    public class CommandExecutedEventArgs : EventArgs
    {
        public string? CommandName { get; set; }
        public string? Arguments { get; set; }
    }
}
