using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day01
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day01.txt";

        public static int GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetIncreaseNumberTimes(lines.ToList());
        }

        public static int GetResultPartTwo()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetIncreasedTimesWithStatic(lines.ToList());
        }

        public static int GetIncreaseNumberTimes(List<string> measurements)
        {
            var increasedCount = 0;
            var previous = 0;
            for (var i = 0; i < measurements.Count; i++)
            {
                if (i == 0)
                {
                    previous = int.Parse(measurements[i]);
                    continue;
                }

                if (previous < int.Parse(measurements[i]))
                {
                    increasedCount++;
                }

                previous = int.Parse(measurements[i]);
            }

            return increasedCount;
        }

        public static int GetIncreasedTimesWithStatic(List<string> measurements)
        {
            var increasedCount = 0;
            var previous = 0;
            for (var i = 0; i < measurements.Count; i++)
            {
                if (i == 0)
                {
                    previous = int.Parse(measurements[i]) + int.Parse(measurements[i + 1]) + int.Parse(measurements[i + 2]);
                    continue;
                }

                if (i >= measurements.Count - 2)
                {
                    break;
                }

                if (previous < int.Parse(measurements[i]) + int.Parse(measurements[i + 1]) + int.Parse(measurements[i + 2]))
                {
                    increasedCount++;
                }

                previous = int.Parse(measurements[i]) + int.Parse(measurements[i + 1]) + int.Parse(measurements[i + 2]);
            }

            return increasedCount;
        }
    }
}
