using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public static class Day01
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day01.txt";

        public static int GetPart1Result()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetSum(lines.ToList());
        }

        public static int GetPart2Result()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetDuplicateFrequency(lines.ToList());
        }

        public static int GetSum(List<string> lines)
        {
            var sum = 0;

            if (lines.Count > 0)
            {
                foreach (var input in lines)
                {
                    var number = GetNumber(input);
                    sum += number;
                }
            }

            return sum;
        }

        public static int GetDuplicateFrequency(List<string> lines)
        {
            var sum = 0;
            var frequencyChanges = new List<int>();
            var firstDuplicate = 0;
            var foundDuplicate = false;

            if (lines.Count > 0)
            {
                while (!foundDuplicate)
                {
                    foreach (var input in lines)
                    {
                        var number = GetNumber(input);
                        sum += number;

                        if (frequencyChanges.Contains(sum))
                        {
                            firstDuplicate = sum;
                            foundDuplicate = true;
                            break;
                        }

                        frequencyChanges.Add(sum);
                    }
                }
            }

            return firstDuplicate;
        }

        public static int GetNumber(string input)
        {
            int result;
            var op = input.Substring(0, 1);
            var number = int.Parse(input.Substring(1, input.Length - 1));

            if (op == "+")
            {
                result = number;
            }
            else
            {
                result = 0 - number;
            }

            return result;
        }
    }
}
