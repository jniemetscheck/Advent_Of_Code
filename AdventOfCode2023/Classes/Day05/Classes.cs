using System.Collections.Generic;

namespace AdventOfCode2023.Classes.Day05
{
    public class Garden
    {
        public Dictionary<double, double> InitialSeedRanges { get; set; }
        public List<double> InitialSeeds { get; set; }
        public List<LocationMap> LocationMaps { get; set; }
    }

    public class LocationMap
    {
        public int SortOrder { get; set; }
        public List<Map> Maps { get; set; }
    }

    public class Map
    {
        public double DestinationRangeStart { get; set; }
        public double SourceRangeStart { get; set; }
        public double Range { get; set; }
    }
}