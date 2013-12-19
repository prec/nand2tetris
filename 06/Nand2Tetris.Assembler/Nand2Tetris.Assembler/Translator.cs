using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nand2Tetris.Assembler.Models;

namespace Nand2Tetris.Assembler
{
    public class Translator
    {
        public List<string> Translate(List<ParsedCommand> parsedCommands, Dictionary<string, int> symbolTable)
        {
            List<string> codedCommands = new List<string>();

            foreach (var pc in parsedCommands)
            {
                string codedCommand = "";

                switch (pc.CommandType)
                {
                    case CommandType.A_COMMAND:
                        if (symbolTable.ContainsKey(pc.Symbol)) pc.Symbol = symbolTable[pc.Symbol].ToString();
                        codedCommand = GetAddressInstruction(pc);
                        codedCommands.Add(codedCommand);
                        break;
                    case CommandType.C_COMMAND:
                        codedCommand = string.Format("111{0}{1}{2}", GetComputation(pc), GetDestination(pc),
                            GetJump(pc));
                        codedCommands.Add(codedCommand);
                        break;
                }
            }

            return codedCommands;
        }
        public List<string> Translate(List<ParsedCommand> parsedCommands)
        {
            List<string> codedCommands = new List<string>();

            foreach (var pc in parsedCommands)
            {
                string codedCommand = "";

                switch (pc.CommandType)
                {
                    case CommandType.A_COMMAND:
                        codedCommand = GetAddressInstruction(pc);
                        break;
                    case CommandType.C_COMMAND:
                        codedCommand = string.Format("111{0}{1}{2}", GetComputation(pc), GetDestination(pc),
                            GetJump(pc));
                        break;
                }

                codedCommands.Add(codedCommand);
            }

            return codedCommands;
        }

        private string GetAddressInstruction(ParsedCommand pc)
        {
            int symbol = int.Parse(pc.Symbol);

            string leadingZeroes = "";
            string binarySymbol = Convert.ToString(symbol, 2);

            for (int i = 0; i < (16 - binarySymbol.Length); i++)
            {
                leadingZeroes += "0";
            }

            return leadingZeroes + binarySymbol;
        }

        private string GetDestination(ParsedCommand pc)
        {
            string destBits = "";

            if (!string.IsNullOrEmpty(pc.Dest))
            {
                if (pc.Dest.Contains("A")) destBits += "1";
                else destBits += "0";

                if (pc.Dest.Contains("D")) destBits += "1";
                else destBits += "0";

                if (pc.Dest.Contains("M")) destBits += "1";
                else destBits += "0";
            }
            else
            {
                destBits = "000";
            }

            return destBits;
        }

        private string GetComputation(ParsedCommand pc)
        {
            string compBits = "";

            if (pc.Comp.IndexOf('A') >= 0) compBits += "0";
            else if (pc.Comp.IndexOf('M') >= 0) compBits += "1";
            else if (pc.Comp == "D") compBits += "0";
            else compBits += "0";

            switch (pc.Comp)
            {
                case "0":
                    compBits += "101010";
                    break;
                case "1":
                    compBits += "111111";
                    break;
                case "-1":
                    compBits += "111010";
                    break;
                case "D":
                    compBits += "001100";
                    break;
                case "A":
                case "M":
                    compBits += "110000";
                    break;
                case "!D":
                    compBits += "001101";
                    break;
                case "!A":
                case "!M":
                    compBits += "110001";
                    break;
                case "-D":
                    compBits += "001111";
                    break;
                case "-A":
                case "-M":
                    compBits += "110011";
                    break;
                case "D+1":
                    compBits += "011111";
                    break;
                case "A+1":
                case "M+1":
                    compBits += "110111";
                    break;
                case "D-1":
                    compBits += "001110";
                    break;
                case "A-1":
                case "M-1":
                    compBits += "110010";
                    break;
                case "D+A":
                case "D+M":
                    compBits += "000010";
                    break;
                case "D-A":
                case "D-M":
                    compBits += "010011";
                    break;
                case "A-D":
                case "M-D":
                    compBits += "000111";
                    break;
                case "D&A":
                case "D&M":
                    compBits += "000000";
                    break;
                case "D|A":
                case "D|M":
                    compBits += "010101";
                    break;
            }

            return compBits;
        }

        private string GetJump(ParsedCommand pc)
        {
            string jumpBits = "";

            switch (pc.Jump)
            {
                case "JGT":
                    jumpBits = "001";
                    break;
                case "JEQ":
                    jumpBits = "010";
                    break;
                case "JGE":
                    jumpBits = "011";
                    break;
                case "JLT":
                    jumpBits = "100";
                    break;
                case "JNE":
                    jumpBits = "101";
                    break;
                case "JLE":
                    jumpBits = "110";
                    break;
                case "JMP":
                    jumpBits = "111";
                    break;
                default:
                    jumpBits = "000";
                    break;
            }

            return jumpBits;
        }
    }
}
