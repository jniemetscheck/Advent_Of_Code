using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2017.Tests
{
    [TestClass]
    public class Day10Tests
    {
        [TestMethod]
        public void TestOne()
        {
            var input = new List<int> { 3, 4, 1, 5 };
            Assert.AreEqual(12, Days.Day10.GetPart1Result(5, input));
        }

        //[TestMethod]
        //public void TestTwo()
        //{
        //    var input = new List<int> { 7, 4, 8, 9 };
        //    Assert.AreEqual(12, Days.Day10.GetPart1Result(10, input));
        //}
    }
}
