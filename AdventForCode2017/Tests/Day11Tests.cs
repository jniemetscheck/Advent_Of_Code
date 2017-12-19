using AdventOfCode2017.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Tests
{
    [TestClass]
    public class Day11Tests
    {
        [TestMethod]
        public void TestOne()
        {
            Assert.AreEqual(3, Day11.GetResult("ne,ne,ne".Split(',').ToList()));
        }
        [TestMethod]
        public void TestTwo()
        {
            Assert.AreEqual(0, Day11.GetResult("ne,ne,sw,sw".Split(',').ToList()));
        }
        [TestMethod]
        public void TestThree()
        {
            Assert.AreEqual(2, Day11.GetResult("ne,ne,s,s".Split(',').ToList()));
        }
        [TestMethod]
        public void TestFour()
        {
            Assert.AreEqual(3, Day11.GetResult("se,sw,se,sw,sw".Split(',').ToList()));
        }
    }
}
