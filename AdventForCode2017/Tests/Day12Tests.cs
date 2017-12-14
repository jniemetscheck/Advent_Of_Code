using AdventOfCode2017.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2017.Tests
{
    [TestClass]
    public class Day12Tests
    {
        [TestMethod]
        public void TestOne()
        {
            var input = new Dictionary<int, List<int>>();
            input.Add(0, new List<int> { 2 });
            input.Add(1, new List<int> { 1 });
            input.Add(2, new List<int> { 0, 3, 4 });
            input.Add(3, new List<int> { 2, 4 });
            input.Add(4, new List<int> { 2, 3, 6 });
            input.Add(5, new List<int> { 6 });
            input.Add(6, new List<int> { 4, 5 });

            Assert.AreEqual(6, Day12.GetResult(input));
        }
    }
}
