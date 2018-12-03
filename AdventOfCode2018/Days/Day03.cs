using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public static class Day03
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day03.txt";

        public static int GetPart1Result()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetOverlappingCount(lines.ToList());
        }

        public static int GetOverlappingCount(List<string> lines)
        {
            return 0;
        }
    }
}
