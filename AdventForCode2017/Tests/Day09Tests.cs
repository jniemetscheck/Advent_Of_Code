using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AdventOfCode2017.Tests
{
    [TestClass]
    public class Day09Tests
    {
        [TestMethod]
        public void Part1_Test_One()
        {
            var result = Days.Day09.GetResult("{}".Select(c => c.ToString()).ToList());

            Assert.AreEqual(1, result.Score);
        }

        [TestMethod]
        public void Part1_Test_Two()
        {
            var result = Days.Day09.GetResult("{{{}}}".Select(c => c.ToString()).ToList());

            Assert.AreEqual(6, result.Score);
        }

        [TestMethod]
        public void Part1_Test_Three()
        {
            var result = Days.Day09.GetResult("{{},{}}".Select(c => c.ToString()).ToList());

            Assert.AreEqual(5, result.Score);
        }

        [TestMethod]
        public void Part1_Test_Four()
        {
            var result = Days.Day09.GetResult("{{{},{},{{}}}}".Select(c => c.ToString()).ToList());

            Assert.AreEqual(16, result.Score);
        }

        [TestMethod]
        public void Part1_Test_Five()
        {
            var result = Days.Day09.GetResult("{<a>,<a>,<a>,<a>}".Select(c => c.ToString()).ToList());

            Assert.AreEqual(1, result.Score);
        }

        [TestMethod]
        public void Part1_Test_Six()
        {
            var result = Days.Day09.GetResult("{{<ab>},{<ab>},{<ab>},{<ab>}}".Select(c => c.ToString()).ToList());

            Assert.AreEqual(9, result.Score);
        }

        [TestMethod]
        public void Part1_Test_Seven()
        {
            var result = Days.Day09.GetResult("{{<!!>},{<!!>},{<!!>},{<!!>}}".Select(c => c.ToString()).ToList());

            Assert.AreEqual(9, result.Score);
        }

        [TestMethod]
        public void Part1_Test_Eight()
        {
            var result = Days.Day09.GetResult("{{<a!>},{<a!>},{<a!>},{<ab>}}".Select(c => c.ToString()).ToList());

            Assert.AreEqual(3, result.Score);
        }

        [TestMethod]
        public void Part2_Test_One()
        {
            var result = Days.Day09.GetResult("<>".Select(c => c.ToString()).ToList());

            Assert.AreEqual(0, result.GarbageCount);
        }

        [TestMethod]
        public void Part2_Test_Two()
        {
            var result = Days.Day09.GetResult("<random characters>".Select(c => c.ToString()).ToList());

            Assert.AreEqual(17, result.GarbageCount);
        }

        [TestMethod]
        public void Part2_Test_Three()
        {
            var result = Days.Day09.GetResult("<<<<>".Select(c => c.ToString()).ToList());

            Assert.AreEqual(3, result.GarbageCount);
        }

        [TestMethod]
        public void Part2_Test_Four()
        {
            var result = Days.Day09.GetResult("<{!>}>".Select(c => c.ToString()).ToList());

            Assert.AreEqual(2, result.GarbageCount);
        }

        [TestMethod]
        public void Part2_Test_Five()
        {
            var result = Days.Day09.GetResult("<!!>".Select(c => c.ToString()).ToList());

            Assert.AreEqual(0, result.GarbageCount);
        }

        [TestMethod]
        public void Part2_Test_Six()
        {
            var result = Days.Day09.GetResult("<!!!>>".Select(c => c.ToString()).ToList());

            Assert.AreEqual(0, result.GarbageCount);
        }

        [TestMethod]
        public void Part2_Test_Seven()
        {
            var result = Days.Day09.GetResult("<{o\"i!a,<{i<a>".Select(c => c.ToString()).ToList());
            
            Assert.AreEqual(10, result.GarbageCount);
        }
    }
}
