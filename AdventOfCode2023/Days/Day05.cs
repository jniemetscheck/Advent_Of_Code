using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;

namespace AdventOfCode2023.Days
{
    public static class Day05
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day05.txt";

        public static double GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);
            var garden = GetMappedGarden(lines.ToList());

            //return GetWinnings(cards);
            return 0;
        }

        //public static double GetResultPartTwo()
        //{
        //    var lines = File.ReadAllLines(FilePath);
        //    var cards = GetMappedCards(lines.ToList());

        //    return GetWinningsModified(cards);
        //}

        public static Garden GetMappedGarden(List<string> lines)
        {
            var garden = new Garden { InitialSeeds = new List<int>(), LocationMaps = new List<LocationMap>() };

            var currentSortOrder = 1;

            LocationMap currentLocationMap = null;
            for (var i = 0; i < lines.Count; i++)
            {
                if (i == 0)
                {
                    //inital seed line
                    var seedLineSplit = lines[i].Split(':');
                    var seedsSplit = seedLineSplit[1].Trim().Split(' ');

                    foreach (var seed in seedsSplit)
                    {
                        garden.InitialSeeds.Add(int.Parse(seed));
                    }
                }
                else
                {
                    if (i >= 2)
                    {
                        if (lines[i].Trim().Length > 0)
                        {
                            //ok, we can start processing
                            if (!char.IsDigit(lines[i][0]))
                            {
                                if (currentLocationMap != null)
                                {
                                    garden.LocationMaps.Add(currentLocationMap);
                                }
                                //start of new category
                                currentLocationMap = new LocationMap { SortOrder = currentSortOrder, Maps = new List<Map>() };
                                currentSortOrder++;
                            }
                            else
                            {
                                var rangeSplit = lines[i].Split(' ');

                                currentLocationMap.Maps.Add(new Map { DestinationRangeStart = int.Parse(rangeSplit[0]), SourceRangeStart = int.Parse(rangeSplit[1]), Range = int.Parse(rangeSplit[2]) });
                            }
                        }
                    }
                }
            }

            return garden;
        }
    }

    public class Garden
    {
        public List<int> InitialSeeds { get; set; }
        public List<LocationMap> LocationMaps { get; set; }
    }

    public class LocationMap
    {
        public int SortOrder { get; set; }
        public List<Map> Maps { get; set; }
    }

    public class Map
    {
        public int DestinationRangeStart { get; set; }
        public int SourceRangeStart { get; set; }
        public int Range { get; set; }
    }
}