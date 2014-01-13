using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nand2Tetris.VmTranslator.Models;

namespace Nand2Tetris.VmTranslator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (string.IsNullOrEmpty(args[0])) throw new Exception("No file name");

            string inputFile = args[0];
            string outputFile = GetOutputFilePath(inputFile);

            Parser p = new Parser(inputFile);
            CodeWriter cw = new CodeWriter();

            List<ParsedCommand> parsedCommands = p.Parse();
            List<string> codedCommands = cw.Write(parsedCommands, Path.GetFileNameWithoutExtension(inputFile));

            File.WriteAllLines(outputFile, codedCommands.ToArray());
        }

        private static string GetOutputFilePath(string inputFile)
        {
            string outputFileName = Path.GetFileNameWithoutExtension(inputFile);
            string outputFilePath = Path.GetDirectoryName(inputFile);

            return string.Format(@"{0}\{1}.asm", outputFilePath, outputFileName);
        }
    }
}
