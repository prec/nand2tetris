using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nand2Tetris.VmTranslator.Translators
{
    public class EqualityOperationTranslator
    {
        public string TranslateEqualityOperatorToJumpInstruction(string comparisonSymbol)
        {
            switch (comparisonSymbol)
            {
                case "<":
                    return "D;JLT";
                case "<=":
                    return "D;JLE";
                case "==":
                    return "D;JEQ";
                case ">=":
                    return "D;JGE";
                case ">":
                    return "D;JGT";
                case "!=":
                    return "D;JNE";
            }

            throw new Exception("Not a valid comparison operator");
        }
    }
}
