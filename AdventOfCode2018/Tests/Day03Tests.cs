using AdventOfCode2018.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2018.Tests
{
    [TestClass]
    public class Day03Tests
    {
        [TestMethod]
        public void Part1_TestOne()
        {
            var lines = new List<string> { "#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2" };

            Assert.AreEqual(4, Day03.GetResult(lines).OverlappingCount);
        }

        [TestMethod]
        public void Part2_TestOne()
        {
            var lines = new List<string> { "#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2" };

            Assert.AreEqual(3, Day03.GetResult(lines).NonOverlappingClaimId);
        }
    }
}
