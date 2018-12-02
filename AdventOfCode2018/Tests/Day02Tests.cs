using AdventOfCode2018.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2018.Tests
{
    [TestClass]
    public class Day02Tests
    {
        [TestMethod]
        public void Part1_TestOne()
        {
            var lines = new List<string> { "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee", "ababab" };

            Assert.AreEqual(12, Day02.GetChecksum(lines));
        }

        [TestMethod]
        public void Part2_TestOne()
        {
            var lines = new List<string> { "abcde", "fghij", "klmno", "pqrst", "fguij", "axcye", "wvxyz" };

            Assert.AreEqual("fgij", Day02.GetCommonId(lines));
        }
    }
}
