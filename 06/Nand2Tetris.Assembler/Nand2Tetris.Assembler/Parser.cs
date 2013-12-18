using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nand2Tetris.Assembler.Models;
using CommandType = Nand2Tetris.Assembler.Models.CommandType;

namespace Nand2Tetris.Assembler
{
    public class Parser
    {
        private List<string> _commands;
        public Parser(string fileName)
        {
            _commands = new List<string>(File.ReadAllLines(fileName));
        }

        public List<ParsedCommand> Parse()
        {
            List<ParsedCommand> parsedCommands = new List<ParsedCommand>();

            _commands.RemoveAll(c => string.IsNullOrEmpty(c) || IsComment(c));

            foreach (var command in _commands)
            {
                ParsedCommand pc = new ParsedCommand();

                pc.CommandType = GetCommandType(command);

                switch (pc.CommandType)
                {
                    case CommandType.A_COMMAND:
                    case CommandType.L_COMMAND:
                        pc.Symbol = GetSymbol(command);
                        break;
                    case CommandType.C_COMMAND:
                        pc.Dest = GetDestination(command);
                        pc.Comp = GetComputation(command);
                        pc.Jump = GetJump(command);
                        break;
                }
                parsedCommands.Add(pc);
            }

            return parsedCommands;
        }

        private bool IsComment(string command)
        {
            char firstChar = command[0];
            char secondChar = command[1];

            return (firstChar == '/' && secondChar == '/');
        }

        private CommandType GetCommandType(string command)
        {
            if (IsACommand(command)) return CommandType.A_COMMAND;
            if (IsCCommand(command)) return CommandType.C_COMMAND;
            if (IsLCommand(command)) return CommandType.L_COMMAND;

            throw new Exception("Bad command");
        }

        private bool IsACommand(string command)
        {
            return command[0] == '@';
        }

        private bool IsCCommand(string command)
        {
            char firstChar = command[0];

            return firstChar == 'A' || firstChar == 'M' || firstChar == 'D' || firstChar == '0';
        }

        private bool IsLCommand(string command)
        {
            char firstChar = command[0];

            return firstChar == '(';
        }

        private string GetSymbol(string command)
        {
            char firstChar = command[0];

            if (firstChar == '@')
                return command.Substring(1);
            if (firstChar == '(')
                return command.Substring(1, command.IndexOf(')') - 1);

            throw new Exception("Bad command");
        }

        private string GetDestination(string command)
        {
            if (command.IndexOf('=') == -1) return "";
            return command.Substring(0, command.IndexOf('='));
        }

        private string GetComputation(string command)
        {
            string computation = "";
            
            if (command.IndexOf('=') >= 0) computation = command.Substring(command.IndexOf('=') + 1);
            if (command.IndexOf(';') >= 0) computation = command.Substring(0, command.IndexOf(';'));

            return computation;
        }

        private string GetJump(string command)
        {
            if (command.IndexOf(';') == -1) return "";
            return command.Substring(command.IndexOf(';') + 1);
        }
    }
}
