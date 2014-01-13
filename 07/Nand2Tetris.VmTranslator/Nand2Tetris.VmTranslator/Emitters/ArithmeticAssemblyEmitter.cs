using System.Collections.Generic;
using Nand2Tetris.VmTranslator.Helpers;

namespace Nand2Tetris.VmTranslator.Emitters
{
    public class ArithmeticAssemblyEmitter
    {
        private StackAssemblyHelper _helper;
        private SubroutineAssemblyEmitter _subroutineEmitter;

        private bool _includeLtSubroutine;
        private bool _includeGtSubroutine;
        private bool _includeEqSubroutine;

        public ArithmeticAssemblyEmitter()
        {
            _helper = new StackAssemblyHelper();
            _subroutineEmitter = new SubroutineAssemblyEmitter();

            _includeEqSubroutine = false;
            _includeGtSubroutine = false;
            _includeLtSubroutine = false;
        }

        public List<string> EmitAdd()
        {
            return EmitBinomialExpression("D=D+M");
        }

        public List<string> EmitSub()
        {
            return EmitBinomialExpression("D=D+M", negateSecondOperand: true);
        }

        public string[] EmitLt()
        {
            if (!_includeLtSubroutine) _includeLtSubroutine = true;

            return _subroutineEmitter.EmitLtSubroutineCall();
        }

        public string[] EmitGt()
        {
            if (!_includeGtSubroutine) _includeGtSubroutine = true;

            return _subroutineEmitter.EmitGtSubroutineCall();
        }

        public string[] EmitEq()
        {
            if (!_includeEqSubroutine) _includeEqSubroutine = true;

            return _subroutineEmitter.EmitEqSubroutineCall();
        }

        public List<string> EmitAnd()
        {
            return EmitBinomialExpression("D=D&M");
        }

        public List<string> EmitOr()
        {
            return EmitBinomialExpression("D=D|M");
        }

        public List<string> EmitNeg()
        {
            return EmitMonomialExpression("D=-M");
        }

        public List<string> EmitNot()
        {
            return EmitMonomialExpression("D=!M");
        }

        public List<string> EmitSubroutines()
        {
            List<string> instructions = new List<string>();

            if (_includeEqSubroutine) instructions.AddRange(_subroutineEmitter.EmitEqSubroutine());
            if (_includeGtSubroutine) instructions.AddRange(_subroutineEmitter.EmitGtSubroutine());
            if (_includeLtSubroutine) instructions.AddRange(_subroutineEmitter.EmitLtSubroutine());

            return instructions;
        }

        private List<string> EmitBinomialExpression(string operationCommand, bool negateSecondOperand = false)
        {
            List<string> instructions = new List<string>();

            instructions.AddRange(_helper.DecrementStackPointer);

            if (negateSecondOperand) instructions.Add("D=-M");
            else instructions.Add("D=M");

            instructions.AddRange(_helper.DecrementStackPointer);
            instructions.Add(operationCommand);
            instructions.AddRange(_helper.SetTopOfStackToD);
            instructions.AddRange(_helper.IncrementStackPointer);

            return instructions;
        }

        private List<string> EmitMonomialExpression(string operationCommand)
        {
            List<string> instructions = new List<string>();

            instructions.AddRange(_helper.DecrementStackPointer);
            instructions.Add(operationCommand);
            instructions.AddRange(_helper.SetTopOfStackToD);
            instructions.AddRange(_helper.IncrementStackPointer);

            return instructions;
        }
    }
}