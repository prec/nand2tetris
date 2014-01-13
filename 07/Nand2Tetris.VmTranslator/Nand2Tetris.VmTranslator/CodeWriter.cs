using System.Collections.Generic;
using Nand2Tetris.VmTranslator.Emitters;
using Nand2Tetris.VmTranslator.Helpers;
using Nand2Tetris.VmTranslator.Models;
using Nand2Tetris.VmTranslator.Optimizers;

namespace Nand2Tetris.VmTranslator
{
    public class CodeWriter
    {
        private BootstrapAssemblyHelper _bootstrapAssemblyHelper;

        public CodeWriter()
        {
            _bootstrapAssemblyHelper = new BootstrapAssemblyHelper();
        }

        public List<string> Write(List<ParsedCommand> parsedCommands, string fileName)
        {
            StackAssemblyEmitter stackAssemblyEmitter = new StackAssemblyEmitter();
            ArithmeticAssemblyEmitter arithmeticAssemblyEmitter = new ArithmeticAssemblyEmitter();
            AssemblyInstructionOptimizer optimizer = new AssemblyInstructionOptimizer();

            List<string> codedInstructions = new List<string>();

            foreach (var pc in parsedCommands)
            {
                switch (pc.CommandType)
                {
                    case CommandType.C_POP:
                        codedInstructions.AddRange(stackAssemblyEmitter.EmitPop(pc.Arg1, pc.Arg2, fileName));
                        break;
                    case CommandType.C_PUSH:
                        codedInstructions.AddRange(stackAssemblyEmitter.EmitPush(pc.Arg1, pc.Arg2, fileName));
                        break;
                    case CommandType.C_ARITHMETIC:
                        codedInstructions.AddRange(WriteArithmeticInstructions(pc.Command, arithmeticAssemblyEmitter));
                        break;
                }
            }

            codedInstructions.AddRange(_bootstrapAssemblyHelper.EndLoop);
            codedInstructions.AddRange(arithmeticAssemblyEmitter.EmitSubroutines());

            codedInstructions = optimizer.Optimize(codedInstructions);

            return codedInstructions;
        }

        private List<string> WriteArithmeticInstructions(string operation, ArithmeticAssemblyEmitter emitter)
        {
            List<string> instructions = new List<string>();

            switch (operation)
            {
                case "add":
                    instructions.AddRange(emitter.EmitAdd());
                    break;
                case "sub":
                    instructions.AddRange(emitter.EmitSub());
                    break;
                case "neg":
                    instructions.AddRange(emitter.EmitNeg());
                    break;
                case "eq":
                    instructions.AddRange(emitter.EmitEq());
                    break;
                case "gt":
                    instructions.AddRange(emitter.EmitGt());
                    break;
                case "lt":
                    instructions.AddRange(emitter.EmitLt());
                    break;
                case "and":
                    instructions.AddRange(emitter.EmitAnd());
                    break;
                case "or":
                    instructions.AddRange(emitter.EmitOr());
                    break;
                case "not":
                    instructions.AddRange(emitter.EmitNot());
                    break;
            }

            return instructions;
        }
    }
}