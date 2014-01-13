using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nand2Tetris.VmTranslator.Helpers
{
    public class StackAssemblyHelper
    {
        public string[] IncrementStackPointer { get; private set; }

        public string[] DecrementStackPointer { get; private set; }
        public string[] SetTopOfStackToD { get; private set; }

        public StackAssemblyHelper()
        {
            InitializeProperties();
        }

        private void InitializeProperties()
        {
            InitIncrementStackPointer();
            InitDecrementStackPointer();
            InitSetTopOfStackToD();
        }

        private void InitIncrementStackPointer()
        {
            IncrementStackPointer = new []
                   {
                       "@SP",
                       "AM=M+1"
                   };
        }

        private void InitDecrementStackPointer()
        {
            DecrementStackPointer = new []
                   {
                       "@SP",
                       "AM=M-1"
                   };
        }

        private void InitSetTopOfStackToD()
        {
            SetTopOfStackToD = new []
                   {
                       "@SP",
                       "A=M",
                       "M=D"
                   };
        }
    }
}
