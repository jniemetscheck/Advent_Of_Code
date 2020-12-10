using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day10
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day10.txt";

        public static int GetResultPartOne()
        {
            var input = File.ReadAllLines(FilePath).Select(int.Parse).ToList();

            return GetJoltCount(input);
        }

        public static int GetJoltCount(List<int> adapters)
        {
            var oneJoltDifferences = 0;
            var threeJoltDifferences = 0;
            var currentJoltage = 0;

            foreach (var adapter in adapters.OrderBy(o => o))
            {
                if (adapter - currentJoltage == 1)
                {
                    oneJoltDifferences++;
                }

                if (adapter - currentJoltage == 3)
                {
                    threeJoltDifferences++;
                }

                currentJoltage = adapter;
            }

            //account for highest adapter
            threeJoltDifferences++;

            return oneJoltDifferences * threeJoltDifferences;
        }
    }
}
