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
    public class Day19Tests
    {
        [TestMethod]
        public void TestOne()
        {
            var map = new List<List<string>>();
            map.Add("    |         ".Select(c => c.ToString()).ToList());
            map.Add("    |  +--+   ".Select(c => c.ToString()).ToList());
            map.Add("    A  |  C   ".Select(c => c.ToString()).ToList());
            map.Add("F---|----E|--+".Select(c => c.ToString()).ToList());
            map.Add("    |  |  |  D".Select(c => c.ToString()).ToList());
            map.Add("    +B-+  +--+".Select(c => c.ToString()).ToList());

            Assert.AreEqual("ABCDEF", Day19.GetPart1Result(map));
        }
    }
}
