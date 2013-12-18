using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nand2Tetris.Assembler.Models
{
    public class ParsedCommand
    {
        public CommandType CommandType { get; set; }
        public string Symbol { get; set; }
        public string Dest { get; set; }
        public string Comp { get; set; }
        public string Jump { get; set; }
    }
}
