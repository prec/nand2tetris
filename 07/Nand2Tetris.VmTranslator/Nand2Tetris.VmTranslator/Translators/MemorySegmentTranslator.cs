using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nand2Tetris.VmTranslator.Translators
{
    public class MemorySegmentTranslator
    {
        public string TranslateSegmentToAddressInstruction(string segment)
        {
            switch (segment)
            {
                case "local":
                    return "@LCL";
                case "argument":
                    return "@ARG";
                case "this":
                    return "@THIS";
                case "that":
                    return "@THAT";
            }

            throw new Exception("Unknown memory segment");
        }
    }
}
