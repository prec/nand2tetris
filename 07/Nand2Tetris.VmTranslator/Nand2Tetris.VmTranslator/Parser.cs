using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nand2Tetris.VmTranslator.Models;

namespace Nand2Tetris.VmTranslator
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

            CleanCommands();

            foreach (var command in _commands)
            {
                ParsedCommand pc = new ParsedCommand();

                string[] splitCommand = command.Split(' ');

                pc.CommandType = GetCommandType(splitCommand[0]);

                SetArguments(pc, splitCommand);

                parsedCommands.Add(pc);
            }
            return parsedCommands;
        }

        private void CleanCommands()
        {
            _commands.RemoveAll(c => string.IsNullOrEmpty(c) || IsComment(c));

            _commands = (from c in _commands
                         select c.Contains("//") ? c.Substring(0, c.IndexOf("//")).Trim() : c.Trim()).ToList();
        }

        private bool IsComment(string command)
        {
            char firstChar = command[0];
            char secondChar = command[1];

            return (firstChar == '/' && secondChar == '/');
        }

        private CommandType GetCommandType(string command)
        {
            command = command.Replace(" ", "");

            switch (command)
            {
                case "add":
                case "sub":
                case "neg":
                case "eq":
                case "gt":
                case "lt":
                case "and":
                case "or":
                case "not":
                    return CommandType.C_ARITHMETIC;
                case "push":
                    return CommandType.C_PUSH;
                case "pop":
                    return CommandType.C_POP;
                case "label":
                    return CommandType.C_LABEL;
                case "goto":
                    return CommandType.C_GOTO;
                case "if-goto":
                    return CommandType.C_IF;
                case "function":
                    return CommandType.C_FUNCTION;
                case "call":
                    return CommandType.C_CALL;
                case "return":
                    return CommandType.C_RETURN;
                default:
                    throw new Exception("Bad command");
            }
        }

        private void SetArguments(ParsedCommand pc, string[] splitCommand)
        {
            switch (pc.CommandType)
            {
                case CommandType.C_ARITHMETIC:
                    pc.Command = splitCommand[0];
                    break;
                case CommandType.C_LABEL:
                case CommandType.C_GOTO:
                case CommandType.C_IF:
                    pc.Arg1 = splitCommand[1];
                    break;
                case CommandType.C_FUNCTION:
                case CommandType.C_CALL:
                case CommandType.C_PUSH:
                case CommandType.C_POP:
                    pc.Arg1 = splitCommand[1];
                    pc.Arg2 = splitCommand[2];
                    break;
            }
        }
    }
}
