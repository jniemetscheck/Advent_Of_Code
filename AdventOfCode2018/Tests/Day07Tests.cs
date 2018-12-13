using System.Collections.Generic;
using AdventOfCode2018.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2018.Tests
{
    [TestClass]
    public class Day07Tests
    {
        [TestMethod]
        public void Part1_TestOne()
        {
            var lines = new List<string>
            {
                "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
                "Step A must be finished before step B can begin.",
                "Step A must be finished before step D can begin.",
                "Step B must be finished before step E can begin.",
                "Step D must be finished before step E can begin.",
                "Step F must be finished before step E can begin."
            };

            Assert.AreEqual("CABDFE", Day07.GetCorrectOrder(lines));
        }
    }
}
