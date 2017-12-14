using AdventOfCode2017.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2017.Tests
{
    [TestClass]
    public class Day13Tests
    {
        [TestMethod]
        public void TestOne()
        {
            var entries = new List<FirewallEntry>();
            entries.Add(new FirewallEntry { Depth = 0, Range = 3 });
            entries.Add(new FirewallEntry { Depth = 1, Range = 2 });
            entries.Add(new FirewallEntry { Depth = 4, Range = 4 });
            entries.Add(new FirewallEntry { Depth = 6, Range = 4 });

            Assert.AreEqual(24, Day13.GetResult(entries));
        }
    }
}
