using System.Collections.Generic;

namespace AdventOfCode2023.Classes.Day03
{
    public class Engine
    {
        public List<RegularPart> RegularParts { get; set; }
        public List<SpecialPart> SpecialParts { get; set; }
    }

    public class RegularPart
    {
        public int StartX { get; set; }
        public int EndX { get; set; }
        public int Y { get; set; }
        public int Number { get; set; }
    }

    public class SpecialPart
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsPossibleGear { get; set; }
    }
}