using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventForCode2017.Days
{
    public static class Day06
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day06.txt";

        public static int GetPart1Result()
        {
            var input = File.ReadAllText(FilePath);
            var memoryBanks = input.Split('\t').Select(Int32.Parse).ToList(); ;
            var previousMemoryBanks = new List<string>();

            return GetCycleInfo(memoryBanks, previousMemoryBanks).Count;
        }

        public static int GetPart2Result()
        {
            var input = File.ReadAllText(FilePath);
            var memoryBanks = input.Split('\t').Select(Int32.Parse).ToList(); ;

            var cycleInfo = GetCycleInfo(memoryBanks, new List<string>());

            return GetCycleInfo(cycleInfo.DuplicateMemoryBank, new List<string>()).Count - 1;
        }

        private static (int Count, List<int> DuplicateMemoryBank) GetCycleInfo(List<int> memoryBanks, List<string> previousMemoryBanks)
        {
            var count = 0;
            while (true)
            {
                var max = memoryBanks.Max();
                var maxIndex = GetFirstOccuranceIndex(max, memoryBanks);
                var goUntil = 0;

                var distributionNumber = max / (memoryBanks.Count() - 1) == 0 ? 1 : (int)Math.Floor((decimal)(max / (memoryBanks.Count() - 1)));
                if (max / (memoryBanks.Count() - 1) == 0)
                {
                    goUntil = max % (memoryBanks.Count() - 1);
                }
                else
                {
                    goUntil = distributionNumber * (memoryBanks.Count() - 1);
                }

                var nextIndex = maxIndex == memoryBanks.Count - 1 ? 0 : maxIndex + 1;
                var added = 0;
                while (true)
                {
                    memoryBanks[nextIndex] = memoryBanks[nextIndex] + distributionNumber;
                    nextIndex = nextIndex == memoryBanks.Count - 1 ? 0 : nextIndex + 1;
                    added += distributionNumber;

                    if (added >= goUntil)
                    {
                        memoryBanks[maxIndex] = memoryBanks[maxIndex] - added;
                        break;
                    }
                }
                count++;
                if (previousMemoryBanks.Contains(string.Join(",", memoryBanks)))
                {
                    break;
                }
                else
                {
                    previousMemoryBanks.Add(string.Join(",", memoryBanks));
                }
            }

            return (count, memoryBanks);
        }

        private static int GetFirstOccuranceIndex(int numberToLookFor, List<int> ints)
        {
            for (int i = 0; i < ints.Count; i++)
            {
                if (ints[i] == numberToLookFor)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
