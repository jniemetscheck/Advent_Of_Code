using AdventOfCode2017.Days;
using AdventOfCode2017.Days.Sixteen;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2017.Tests
{
    [TestClass]
    public class Day16Tests
    {
        [TestMethod]
        public void TestOne()
        {
            var sequences = new List<string>() { "a", "b", "c", "d", "e" };
            var danceMoves = new List<DanceMove>();
            danceMoves.Add(new DanceMove { Move = Move.Spin, SpinAmount = 1 });
            danceMoves.Add(new DanceMove { Move = Move.Exchange, ExchangeFirst = 3, ExchangeSecond = 4 });
            danceMoves.Add(new DanceMove { Move = Move.Partner, PartnerFirst = "e", PartnerSecond = "b" });

            Assert.AreEqual("baedc", Day16.GetPart1Result(sequences, danceMoves));
        }
    }
}
