using AdventOfCode2018.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2018.Tests
{
    [TestClass]
    public class Day05Tests
    {
        [TestMethod]
        public void Part1_TestOne()
        {
            const string input = "dabAcCaCBAcCcaDA";

            Assert.AreEqual(10, Day05.GetNumberOfUnitsRemaining(input));
        }

        [TestMethod]
        public void Part2_TestOne()
        {
            const string input = "dabAcCaCBAcCcaDA";

            Assert.AreEqual(4, Day05.GetMinimumUnitsLength(input));
        }
    }
}
