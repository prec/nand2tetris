using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nand2Tetris.Assembler.Models;

namespace Nand2Tetris.Assembler
{
    public class SymbolRepository
    {
        private Dictionary<string, int> _symbolTable;

        private int _nextAvailableAddress;

        public SymbolRepository(List<ParsedCommand> parsedCommands )
        {
            _symbolTable = new Dictionary<string, int>();
            AddPredefinedSymbols();
            _nextAvailableAddress = 16;
        }

        private void AddPredefinedSymbols()
        {
            _symbolTable.Add("SP", 0);
            _symbolTable.Add("LCL", 1);
            _symbolTable.Add("ARG", 2);
            _symbolTable.Add("THIS", 3);
            _symbolTable.Add("THAT", 4);
            _symbolTable.Add("R0", 0);
            _symbolTable.Add("R1", 1);
            _symbolTable.Add("R2", 2);
            _symbolTable.Add("R3", 3);
            _symbolTable.Add("R4", 4);
            _symbolTable.Add("R5", 5);
            _symbolTable.Add("R6", 6);
            _symbolTable.Add("R7", 7);
            _symbolTable.Add("R8", 8);
            _symbolTable.Add("R9", 9);
            _symbolTable.Add("R10", 10);
            _symbolTable.Add("R11", 11);
            _symbolTable.Add("R12", 12);
            _symbolTable.Add("R13", 13);
            _symbolTable.Add("R14", 14);
            _symbolTable.Add("R15", 15);
            _symbolTable.Add("SCREEN", 16384);
            _symbolTable.Add("KBD", 24576);
        }

        private void BuildSymbolTable(List<ParsedCommand> parsedCommands)
        {
            foreach (var parsedCommand in parsedCommands)
            {
                
            }
        }
    }
}
