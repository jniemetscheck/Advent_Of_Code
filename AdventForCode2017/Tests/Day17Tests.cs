using AdventOfCode2017.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2017.Tests
{
    [TestClass]
    public class Day17Tests
    {
        [TestMethod]
        public void TestOne()
        {
            Assert.AreEqual(638, Day17.GetPart1Result(3));
        }
    }
}
