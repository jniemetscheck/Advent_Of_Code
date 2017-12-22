using AdventOfCode2017.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2017.Tests
{
    [TestClass]
    public class Day20Tests
    {
        [TestMethod]
        public void TestOne()
        {
            var particles = new List<Particle>();
            particles.Add(new Particle { Identifier = 0, Position = new Coordinates { X = 3, Y = 0, Z = 0 }, Velocity = new Coordinates { X = 2, Y = 0, Z = 0 }, Acceleration = new Coordinates { X = -1, Y = 0, Z = 0 } });
            particles.Add(new Particle { Identifier = 1, Position = new Coordinates { X = 4, Y = 0, Z = 0 }, Velocity = new Coordinates { X = 0, Y = 0, Z = 0 }, Acceleration = new Coordinates { X = -2, Y = 0, Z = 0 } });

            Assert.AreEqual(0, Day20.GetPart1Result(particles));
        }
    }
}
