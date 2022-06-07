using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentsParser
{
    public class CommandExecutingEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
        public string? CommandName { get; set; }
        public string? Arguments { get; set; }
    }
}
