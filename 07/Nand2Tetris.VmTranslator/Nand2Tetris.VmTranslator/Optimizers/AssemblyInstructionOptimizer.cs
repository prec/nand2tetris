using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nand2Tetris.VmTranslator.Optimizers
{
    public class AssemblyInstructionOptimizer
    {
        public List<string> Optimize(List<string> codedInstructions)
        {
            codedInstructions = RemoveRedundantStackPointerMath(codedInstructions);

            return codedInstructions;
        }

        private List<string> RemoveRedundantStackPointerMath(List<string> codedInstructions)
        {
            for (int i = 0; i < codedInstructions.Count; i++)
            {
                if (codedInstructions[i] == "@SP" && codedInstructions[i + 1] == "AM=M+1" &&
                    codedInstructions[i + 2] == "@SP" && codedInstructions[i + 3] == "AM=M-1")
                {
                    codedInstructions[i] = "";
                    codedInstructions[i + 1] = "";
                    codedInstructions[i + 2] = "";
                    codedInstructions[i + 3] = "";
                }

            }

            return (from ci in codedInstructions
                where !string.IsNullOrEmpty(ci)
                select ci).ToList();
        }
    }
}
