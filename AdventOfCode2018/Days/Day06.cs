using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public static class Day06
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day05.txt";

        public static int GetPart1Result()
        {
            var input = File.ReadAllLines(FilePath);

            return GetLargestNonInfiniteArea(input.ToList());
        }

        public static int GetLargestNonInfiniteArea(List<string> lines)
        {
            var coordinates = GetCoordinates(lines);

            return 0;
        }

        private static List<Coordinate> GetCoordinates(List<string> lines)
        {
            // (p1,p2) and (q1,q2) is |p1 - q1| + |p2 - q2|
            var result = new List<Coordinate>();

            foreach (var line in lines)
            {
                var coordinates = line.Split(',');
                result.Add(new Coordinate { X = int.Parse(coordinates[0].Trim()), Y = int.Parse(coordinates[1].Trim()) });
            }

            return result;
        }

        private class Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
