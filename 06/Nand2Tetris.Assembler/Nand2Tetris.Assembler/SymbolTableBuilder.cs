using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nand2Tetris.Assembler.Models;

namespace Nand2Tetris.Assembler
{
    public class SymbolTableBuilder
    {
        public Dictionary<string, int> Build(List<ParsedCommand> parsedCommands)
        {
            Dictionary<string, int> symbolTable = new Dictionary<string, int>();
                
            AddPredefinedSymbols(symbolTable);
            AddPseudoCommands(parsedCommands, symbolTable);
            AddVariables(parsedCommands, symbolTable);

            return symbolTable;
        }

        private void AddPredefinedSymbols(Dictionary<string, int> symbolTable)
        {
            symbolTable.Add("SP", 0);
            symbolTable.Add("LCL", 1);
            symbolTable.Add("ARG", 2);
            symbolTable.Add("THIS", 3);
            symbolTable.Add("THAT", 4);
            symbolTable.Add("R0", 0);
            symbolTable.Add("R1", 1);
            symbolTable.Add("R2", 2);
            symbolTable.Add("R3", 3);
            symbolTable.Add("R4", 4);
            symbolTable.Add("R5", 5);
            symbolTable.Add("R6", 6);
            symbolTable.Add("R7", 7);
            symbolTable.Add("R8", 8);
            symbolTable.Add("R9", 9);
            symbolTable.Add("R10", 10);
            symbolTable.Add("R11", 11);
            symbolTable.Add("R12", 12);
            symbolTable.Add("R13", 13);
            symbolTable.Add("R14", 14);
            symbolTable.Add("R15", 15);
            symbolTable.Add("SCREEN", 16384);
            symbolTable.Add("KBD", 24576);
        }

        private void AddPseudoCommands(List<ParsedCommand> parsedCommands, Dictionary<string, int> symbolTable)
        {
            int currentCommand = 0;
            int commandOffset = 0;

            foreach (var parsedCommand in parsedCommands)
            {
                if (parsedCommand.CommandType == CommandType.L_COMMAND)
                {
                    symbolTable.Add(parsedCommand.Symbol, currentCommand - commandOffset);
                    commandOffset++;
                }

                currentCommand++;
            }
        }

        private void AddVariables(List<ParsedCommand> parsedCommands, Dictionary<string, int> symbolTable)
        {
            int nextAvailableAddress = 16;

            foreach (var parsedCommand in parsedCommands)
            {
                if (parsedCommand.CommandType == CommandType.A_COMMAND)
                {
                    int parsedSymbol = 0;
                    if (!int.TryParse(parsedCommand.Symbol, out parsedSymbol) && !symbolTable.ContainsKey(parsedCommand.Symbol))
                    {
                        symbolTable.Add(parsedCommand.Symbol, nextAvailableAddress);
                        nextAvailableAddress++;
                    }
                }
            }
        }
    }
}
