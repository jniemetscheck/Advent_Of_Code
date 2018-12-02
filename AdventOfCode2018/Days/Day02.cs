using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public static class Day02
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day02.txt";

        public static int GetPart1Result()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetChecksum(lines.ToList());
        }

        public static string GetPart2Result()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetCommonId(lines.ToList());
        }

        public static int GetChecksum(List<string> lines)
        {
            var exactlyTwoCount = 0;
            var exactlyThreeCount = 0;

            foreach (var line in lines)
            {
                var dict = new Dictionary<char, int>();
                foreach (var letter in line)
                {
                    if (dict.ContainsKey(letter))
                    {
                        dict[letter]++;
                    }
                    else
                    {
                        dict.Add(letter, 1);
                    }
                }

                if (dict.ContainsValue(2))
                {
                    exactlyTwoCount++;
                }

                if (dict.ContainsValue(3))
                {
                    exactlyThreeCount++;
                }
            }

            return exactlyTwoCount * exactlyThreeCount;
        }

        public static string GetCommonId(List<string> lines)
        {
            var dict = new Dictionary<string, List<string>>();
            var closeStrings = string.Empty;
            var minClosest = 0;

            foreach (var line in lines)
            {
                dict.Add(line, line.Select(c => c.ToString()).ToList());
            }

            foreach (var outer in dict)
            {
                foreach (var inner in dict)
                {
                    //don't process yourself
                    if (outer.Key != inner.Key)
                    {
                        var tempCloseString = string.Empty;

                        for (int i = 0; i < outer.Value.Count; i++)
                        {
                            if (outer.Value[i] == inner.Value[i])
                            {
                                tempCloseString+=inner.Value[i];
                            }
                        }

                        if (tempCloseString.Length > minClosest)
                        {
                            minClosest = tempCloseString.Length;
                            closeStrings = tempCloseString;
                        }
                    }
                }
            }


            return closeStrings;
        }
    }
}
