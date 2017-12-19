using AdventOfCode2017.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2017.Tests
{
    [TestClass]
    public class Day18Tests
    {
        [TestMethod]
        public void TestOne()
        {
            var notes = new List<Note>
            {
                new Note { Type = "set", Identifier = "a", Operand = "1" },
                new Note { Type = "add", Identifier = "a", Operand = "2" },
                new Note { Type = "mul", Identifier = "a", Operand = "a" },
                new Note { Type = "mod", Identifier = "a", Operand = "5" },
                new Note { Type = "snd", Identifier = "a" },
                new Note { Type = "set", Identifier = "a", Operand = "0" },
                new Note { Type = "rcv", Identifier = "a" },
                new Note { Type = "jgz", Identifier = "a", Operand = "-1" },
                new Note { Type = "set", Identifier = "a", Operand = "1" },
                new Note { Type = "jgz", Identifier = "a", Operand = "-2" }
            };

            Assert.AreEqual(4, Day18.GetPart1Result(notes));
        }
    }
}
