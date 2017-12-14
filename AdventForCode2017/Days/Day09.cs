using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public static class Day09
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day09.txt";

        public static int GetPart1Result()
        {
            var input = File.ReadAllText(FilePath);

            return GetResult(input.Select(c => c.ToString()).ToList()).Score;
        }

        public static int GetPart2Result()
        {
            var stream = File.ReadAllText(FilePath);

            return GetResult(stream.Select(c => c.ToString()).ToList()).GarbageCount;
        }

        public static (int Score, int GarbageCount) GetResult(List<string> stream)
        {
            var score = 0;
            var groupsStarted = 0;
            var groupMultiplier = 0;
            var ignoring = false;
            var isCancelled = false;
            var garbageCount = 0;

            foreach (var item in stream)
            {
                if (isCancelled)
                {
                    isCancelled = false;
                    continue;
                }
                if (item == "!")
                {
                    isCancelled = true;
                    continue;
                }
                if (item == "<" || item == ">")
                {
                    if (item == "<")
                    {
                        if (!ignoring)
                        {
                            ignoring = true;
                        }
                        else
                        {
                            garbageCount++;
                        }
                    }
                    else
                    {
                        if (!isCancelled)
                        {
                            ignoring = false;
                        }
                    }
                    continue;
                }

                switch (item)
                {
                    case "{":
                        if (!ignoring)
                        {
                            groupsStarted++;
                            groupMultiplier++;
                        }
                        else
                        {
                            garbageCount++;
                        }
                        break;
                    case "}":
                        if (!ignoring)
                        {
                            groupsStarted--;
                            score += groupMultiplier;
                            groupMultiplier--;

                        }
                        else
                        {
                            garbageCount++;
                        }
                        break;
                    case "<":
                        ignoring = true;
                        break;
                    case ">":
                        ignoring = false;
                        break;
                    default:
                        if (ignoring)
                        {
                            garbageCount++;
                        }
                        break;
                }
            }

            return (score, garbageCount);
        }
    }
}
