using System;
using System.Collections.Generic;
using Nand2Tetris.VmTranslator.Helpers;
using Nand2Tetris.VmTranslator.Translators;

namespace Nand2Tetris.VmTranslator.Emitters
{
    public class StackAssemblyEmitter
    {
        private StackAssemblyHelper _helper;
        private MemorySegmentTranslator _translator;

        public StackAssemblyEmitter()
        {
            _helper = new StackAssemblyHelper();
            _translator = new MemorySegmentTranslator();
        }
        public string[] WriteSetStackPointer(int value)
        {
            return new[]
                   {
                       string.Format("@{0}", value),
                       "D=A",
                       "@SP",
                       "M=D"
                   };
        }

        public List<string> EmitPop(string arg1, string arg2, string fileName)
        {
            List<string> instructions = new List<string>();
            int i = int.Parse(arg2);

            instructions.AddRange(_helper.DecrementStackPointer);

            switch (arg1)
            {
                case "temp":
                case "pointer":
                    instructions.Add("D=M");
                    instructions.Add(string.Format("@{0}", GetAddressWithFixedOffset(arg1, i)));
                    instructions.Add("M=D");
                    break;
                case "static":
                    instructions.Add("D=M");
                    instructions.Add(string.Format("@{0}.{1}", fileName, arg2));
                    instructions.Add("M=D");
                    break;
                default:
                    if (i != 0)
                    {
                        instructions.AddRange(EmitCalculateMemorySegmentWithOffset(arg1, arg2));
                        instructions.Add("@SP");
                        instructions.Add("A=M");
                        instructions.Add("D=M");
                        instructions.Add("@R14");
                        instructions.Add("A=M");
                        instructions.Add("M=D");
                    }
                    else
                    {
                        instructions.Add("D=M");
                        instructions.Add(_translator.TranslateSegmentToAddressInstruction(arg1));
                        instructions.Add("A=M");
                        instructions.Add("M=D");
                    }
                    
                    break;
            }

            return instructions;
        }

        public List<string> EmitPush(string arg1, string arg2, string fileName)
        {
            List<string> instructions = new List<string>();
            int i = int.Parse(arg2);

            switch (arg1)
            {
                case "constant":
                    instructions.Add(string.Format("@{0}", arg2));
                    instructions.Add("D=A");                   
                    break;
                case "temp":
                case "pointer":
                    instructions.Add(string.Format("@{0}", GetAddressWithFixedOffset(arg1, i)));
                    instructions.Add("D=M");
                    break;
                case "static":
                    instructions.Add(string.Format("@{0}.{1}", fileName, arg2));
                    instructions.Add("D=M");
                    break;
                default:
                    if (i != 0)
                    {
                        instructions.AddRange(EmitCalculateMemorySegmentWithOffset(arg1, arg2));
                    }
                    else
                    {
                        instructions.Add(_translator.TranslateSegmentToAddressInstruction(arg1));
                        instructions.Add("A=M");
                    }
                    
                    instructions.Add("D=M");
                    break;
            }

            instructions.AddRange(_helper.SetTopOfStackToD);
            instructions.AddRange(_helper.IncrementStackPointer);

            return instructions;
        }

        private int GetAddressWithFixedOffset(string arg1, int i)
        {
            switch (arg1)
            {
                case "temp":
                    return 5 + i;
                case "pointer":
                    return 3 + i;
            }

            throw new Exception("Not a memory segment with a known fixed offset");
        }

        private string[] EmitCalculateMemorySegmentWithOffset(string arg1, string arg2)
        {
            return new[]
                   {
                       string.Format("@{0}", arg2),
                       "D=A",
                       _translator.TranslateSegmentToAddressInstruction(arg1),
                       "D=D+M",
                       "@R14",
                       "M=D",
                       "A=M"
                   };
        }
    }
}
