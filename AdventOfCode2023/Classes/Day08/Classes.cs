using System.Collections.Generic;

namespace AdventOfCode2023.Classes.Day08
{
    public class Map
    {
        public List<string> MovementInstructions { get; set; }
        public List<MapItem> MapItems { get; set; }
    }

    public class MapItem
    {
        public string Origin { get; set; }
        public string LeftDestination { get; set; }
        public string RightDestination { get; set; }
    }
}