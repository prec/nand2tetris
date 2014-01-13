using System.Collections.Generic;
using Nand2Tetris.VmTranslator.Helpers;
using Nand2Tetris.VmTranslator.Translators;

namespace Nand2Tetris.VmTranslator.Emitters
{
    public class SubroutineAssemblyEmitter
    {
        private int _subroutineLabelIndex;
        private StackAssemblyHelper _stackHelper;
        private ArithmeticSubroutineAssemblyHelper _subroutineHelper;
        private EqualityOperationTranslator _translator;

        private const string EQSUBROUTINENAME = "EQ$SUB";
        private const string LTSUBROUTINENAME = "LT$SUB";
        private const string GTSUBROUTINENAME = "GT$SUB";

        public SubroutineAssemblyEmitter()
        {        
            _stackHelper = new StackAssemblyHelper();
            _subroutineHelper = new ArithmeticSubroutineAssemblyHelper();
            _translator = new EqualityOperationTranslator();

            ResetSubroutineIndex();
        }

        public string[] EmitEqSubroutineCall()
        {
            return EmitSubroutineCall(EQSUBROUTINENAME);
        }

        public string[] EmitLtSubroutineCall()
        {
            return EmitSubroutineCall(LTSUBROUTINENAME);
        }

        public string[] EmitGtSubroutineCall()
        {
            return EmitSubroutineCall(GTSUBROUTINENAME);
        }

        private string[] EmitSubroutineCall(string subroutineName)
        {
            string[] instructions = new[]
                                    {
                                        string.Format("@RIP${0}", _subroutineLabelIndex),
                                        "D=A",
                                        string.Format("@{0}", subroutineName),
                                        "0;JMP",
                                        string.Format("(RIP${0})", _subroutineLabelIndex)
                                    };

            _subroutineLabelIndex++;

            return instructions;
        }

        public List<string> EmitEqSubroutine()
        {
            return EmitBinomialAssemblySubroutine(EQSUBROUTINENAME, EmitIfElse("D=D+M", "ISEQ", "=="));
        }

        public List<string> EmitLtSubroutine()
        {
            return EmitBinomialAssemblySubroutine(LTSUBROUTINENAME,
                EmitIfElse("D=D+M", "ISLT", "<"));
        }

        public List<string> EmitGtSubroutine()
        {
            return EmitBinomialAssemblySubroutine(GTSUBROUTINENAME,
                EmitIfElse("D=D+M", "ISGT", ">"));
        }

        private List<string> EmitBinomialAssemblySubroutine(string subroutineName, List<string> operationInstructions)
        {
            List<string> instructions = new List<string>();

            instructions.Add(string.Format("({0})", subroutineName));
            instructions.Add("@R15");
            instructions.Add("M=D");
            instructions.AddRange(_stackHelper.DecrementStackPointer);
            instructions.Add("D=-M");
            instructions.AddRange(_stackHelper.DecrementStackPointer);
            instructions.AddRange(operationInstructions);

            return instructions;
        }

        public void ResetSubroutineIndex()
        {
            _subroutineLabelIndex = 0;
        }

        private List<string> EmitIfElse(string condition, string trueLabel, string comparisonOperation)
        {
            List<string> instructions = new List<string>();

            instructions.Add(condition);
            instructions.Add(string.Format("@{0}", trueLabel));
            instructions.Add(_translator.TranslateEqualityOperatorToJumpInstruction(comparisonOperation));
            instructions.Add("D=0");
            instructions.AddRange(_stackHelper.SetTopOfStackToD);
            instructions.AddRange(_stackHelper.IncrementStackPointer);
            instructions.AddRange(_subroutineHelper.EscapeInstructions);
            instructions.Add(string.Format("({0})", trueLabel));
            instructions.Add("D=-1");
            instructions.AddRange(_stackHelper.SetTopOfStackToD);
            instructions.AddRange(_stackHelper.IncrementStackPointer);
            instructions.AddRange(_subroutineHelper.EscapeInstructions);

            return instructions;
        }
    }
}
