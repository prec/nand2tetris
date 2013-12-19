using System;
using System.Collections.Generic;
using System.IO;
using Nand2Tetris.Assembler.Models;

namespace Nand2Tetris.Assembler.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (string.IsNullOrEmpty(args[0])) throw new Exception("No file name");

            string inputFile = args[0];
            string outputFile = GetOutputFilePath(inputFile);

            Parser p = new Parser(inputFile);
            Translator t = new Translator();
            SymbolTableBuilder b = new SymbolTableBuilder();

            List<ParsedCommand> parsedCommands = p.Parse();
            Dictionary<string, int> symbolTable = b.Build(parsedCommands);
            List<string> codedCommands = t.Translate(parsedCommands, symbolTable);

            File.WriteAllLines(outputFile, codedCommands.ToArray());
        }

        private static string GetOutputFilePath(string inputFile)
        {
            string outputFileName = Path.GetFileNameWithoutExtension(inputFile);
            string outputFilePath = Path.GetDirectoryName(inputFile);
            
            return string.Format(@"{0}\{1}.hack", outputFilePath, outputFileName);
        }
    }
}
