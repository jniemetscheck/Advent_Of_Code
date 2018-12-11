using AdventOfCode2018.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2018.Tests
{
    [TestClass]
    public class Day06Tests
    {
        [TestMethod]
        public void Part1_TestOne()
        {
            var input = new List<string> {
                "1, 1",
                "1, 6",
                "8, 3",
                "3, 4",
                "5, 5",
                "8, 9"

            };

            //d -> a = 5
            //d -> b = 4
            //d -> c = 6
            //d -> e = 3
            //d -> f = 10

            Assert.AreEqual(17, Day06.GetLargestNonInfiniteArea(input));
        }
    }
}
