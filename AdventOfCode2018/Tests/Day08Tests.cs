using AdventOfCode2018.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2018.Tests
{
    [TestClass]
    public class Day08Tests
    {
        [TestMethod]
        public void Part1_TestOne()
        {
            var input = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";

            Assert.AreEqual(138, Day08.GetMetadataSum(input));
        }
    }
}
