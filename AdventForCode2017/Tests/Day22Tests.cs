using NUnit.Framework;
using System.Collections.Generic;

namespace AdventOfCode2017.Tests
{
    [TestFixture]
    public class Day22Tests
    {
        [Test]
        public void TestOne()
        {
            var map = new List<List<Day22.Coordinate>>();
            var rowOne = new List<Day22.Coordinate>();
            rowOne.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            rowOne.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            rowOne.Add(new Day22.Coordinate { CurrentState = Day22.State.Infected });
            map.Add(rowOne);
            var rowTwo = new List<Day22.Coordinate>();
            rowTwo.Add(new Day22.Coordinate { CurrentState = Day22.State.Infected });
            rowTwo.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            rowTwo.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            map.Add(rowTwo);
            var rowThree = new List<Day22.Coordinate>();
            rowThree.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            rowThree.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            rowThree.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            map.Add(rowThree);


            Assert.AreEqual(5587, AdventOfCode2017.Days.Day22.GetResult(map, false));
        }

        [Test]
        public void TestTwo()
        {
            var map = new List<List<Day22.Coordinate>>();
            var rowOne = new List<Day22.Coordinate>();
            rowOne.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            rowOne.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            rowOne.Add(new Day22.Coordinate { CurrentState = Day22.State.Infected });
            map.Add(rowOne);
            var rowTwo = new List<Day22.Coordinate>();
            rowTwo.Add(new Day22.Coordinate { CurrentState = Day22.State.Infected });
            rowTwo.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            rowTwo.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            map.Add(rowTwo);
            var rowThree = new List<Day22.Coordinate>();
            rowThree.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            rowThree.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            rowThree.Add(new Day22.Coordinate { CurrentState = Day22.State.NotInfected });
            map.Add(rowThree);

            Assert.AreEqual(2511944, AdventOfCode2017.Days.Day22.GetResult(map, true));
        }
    }
}
