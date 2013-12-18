using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nand2Tetris.Assembler;
using System.Threading.Tasks;
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

            List<ParsedCommand> parsedCommands = p.Parse();
            List<string> codedCommands = t.Translate(parsedCommands);

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
