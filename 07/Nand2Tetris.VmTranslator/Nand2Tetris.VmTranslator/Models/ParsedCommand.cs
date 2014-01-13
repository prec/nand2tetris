using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nand2Tetris.VmTranslator.Models
{
    public class ParsedCommand
    {
        public CommandType CommandType { get; set; }
        public string Command { get; set; }
        public string Arg1 { get; set; }
        public string Arg2 { get; set; }
    }
}
