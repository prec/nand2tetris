using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nand2Tetris.VmTranslator.Helpers
{
    public class ArithmeticSubroutineAssemblyHelper
    {
        public string[] EscapeInstructions { get; private set; }

        public ArithmeticSubroutineAssemblyHelper()
        {
            InitializeProperties();
        }

        private void InitializeProperties()
        {
            InitEscapeInstructions();
        }

        private void InitEscapeInstructions()
        {
            EscapeInstructions = new string[] { "@R15", "A=M", "0;JMP" };
        }
    }
}
