using System.Collections.Generic;

namespace AdventOfCode2023.Classes.Day02
{
    public class Game
    {
        public int Id { get; set; }
        public List<Set> Sets { get; set; }
        public double Power { get; set; }
    }

    public class Set
    {
        public List<Combination> Combinations { get; set; }
    }

    public class Combination
    {
        public int Count { get; set; }
        public BlockColor Color { get; set; }
    }

    public enum BlockColor
    {
        Blue,
        Green,
        Red
    }
}