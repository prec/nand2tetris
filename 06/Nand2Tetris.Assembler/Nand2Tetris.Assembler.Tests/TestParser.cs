using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nand2Tetris.Assembler.Tests
{
    [TestClass]
    public class TestParser
    {
        private Parser parser;
        private const string FilePath = @"C:\Users\jonasd\Dropbox\nand2tetris\projects\06\add\Add.asm";

        [TestInitialize]
        public void Init()
        {
            parser = new Parser(FilePath);           
        }

        [TestMethod]
        public void CanParse()
        {
            List<string> parsedCommands = parser.Parse();

            Assert.IsNotNull(parsedCommands);
        }
    }
}
