using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nand2Tetris.VmTranslator.Helpers
{
    public class BootstrapAssemblyHelper
    {
        public string[] EndLoop { get; private set; }

        public BootstrapAssemblyHelper()
        {
            InitializeProperties();
        }

        private void InitializeProperties()
        {
            InitEndLoop();
        }

        private void InitEndLoop()
        {
            EndLoop = new []
                   {
                       "(END)",
                       "@END",
                       "0;JMP"
                   };
        }
    }
}
