using AdventOfCode2017.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2017.Tests
{
    [TestClass]
    public class Day15Tests
    {
        [TestMethod]
        public void PartOne_TestOne()
        {
            Assert.AreEqual(588, Day15.GetPart1Result(65, 8921));
        }

        [TestMethod]
        public void PartOne_TestTwo()
        {
            Assert.AreEqual(309, Day15.GetPart2Result(65, 8921));
        }
    }
}
