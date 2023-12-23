using System.Collections.Generic;
using System.IO;
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

            return GetClosestSeedLocation(garden);
        }

        //public static double GetResultPartTwo()
        //{
        //    var lines = File.ReadAllLines(FilePath);
        //    var cards = GetMappedCards(lines.ToList());

        //    return GetWinningsModified(cards);
        //}

        public static Garden GetMappedGarden(List<string> lines)
        {
            var garden = new Garden { InitialSeeds = new List<double>(), LocationMaps = new List<LocationMap>() };

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
                        garden.InitialSeeds.Add(double.Parse(seed));
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

                                currentLocationMap.Maps.Add(new Map { DestinationRangeStart = double.Parse(rangeSplit[0]), SourceRangeStart = double.Parse(rangeSplit[1]), Range = int.Parse(rangeSplit[2]) });
                            }
                        }
                    }
                }
            }

            return garden;
        }

        public static double GetClosestSeedLocation(Garden garden)
        {
            var seedLocations = new List<double>();

            foreach (var seed in garden.InitialSeeds)
            {
                var nextSource = seed;

                foreach (var locationMap in garden.LocationMaps.OrderBy(o => o.SortOrder))
                {
                    nextSource = GetSeedLocation(nextSource, locationMap);
                }

                seedLocations.Add(nextSource);
            }

            return seedLocations.OrderBy(o => o).FirstOrDefault();
        }

        public static double GetSeedLocation(double source, LocationMap map)
        {
            var result = source;

            foreach (var currentMap in map.Maps)
            {
                if (result >= currentMap.SourceRangeStart && result < currentMap.SourceRangeStart + currentMap.Range)
                {
                    if (currentMap.DestinationRangeStart > currentMap.SourceRangeStart)
                    {
                        result += currentMap.DestinationRangeStart - currentMap.SourceRangeStart;
                    }
                    else
                    {
                        result -= currentMap.SourceRangeStart - currentMap.DestinationRangeStart;
                    }

                    break;
                }
            }

            return result;
        }
    }

    public class Garden
    {
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