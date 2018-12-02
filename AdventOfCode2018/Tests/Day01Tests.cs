using System.Collections.Generic;
using AdventOfCode2018.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2018.Tests
{
    [TestClass]
    public class Day01Tests
    {
        [TestMethod]
        public void Part1_Test_One()
        {
            var lines = new List<string> { "+1", "+1", "+1" };

            Assert.AreEqual(3, Day01.GetSum(lines));
        }

        [TestMethod]
        public void Part1_Test_Two()
        {
            var lines = new List<string> { "+1", "+1", "-2" };

            Assert.AreEqual(0, Day01.GetSum(lines));
        }

        [TestMethod]
        public void Part1_Test_Three()
        {
            var lines = new List<string> { "-1", "-2", "-3" };

            Assert.AreEqual(-6, Day01.GetSum(lines));
        }

        [TestMethod]
        public void Part1_Test_Four()
        {
            var lines = new List<string> { "+1", "-2", "+3", "+1" };

            Assert.AreEqual(3, Day01.GetSum(lines));
        }

        [TestMethod]
        public void Part2_Test_One()
        {
            var lines = new List<string> { "+1", "-2", "+3", "+1" };

            Assert.AreEqual(2, Day01.GetDuplicateFrequency(lines));
        }
    }
}
