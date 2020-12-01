using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day01
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day01.txt";

        public static int GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetProductForTwoNumbers(lines.ToList(), 2020);
        }

        public static int GetResultPartTwo()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetProductForThreeNumbers(lines.ToList(), 2020);
        }

        public static int GetProductForTwoNumbers(List<string> lines, int desiredSum)
        {
            var valueOne = 0;
            var valueTwo = 0;

            for (var i = 0; i < lines.Count; i++)
            {
                var found = false;
                valueOne = int.Parse(lines[i]);

                for (var j = 0; j < lines.Count; j++)
                {
                    if (i == j)
                    {
                        break;
                    }

                    valueTwo = int.Parse(lines[j]);

                    if (valueOne + valueTwo == desiredSum)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    break;
                }
            }

            return valueOne * valueTwo;
        }
        public static int GetProductForThreeNumbers(List<string> lines, int desiredSum)
        {
            var valueOne = 0;
            var valueTwo = 0;
            var valueThree = 0;

            for (var i = 0; i < lines.Count; i++)
            {
                var found = false;
                valueOne = int.Parse(lines[i]);

                for (int k = 0; k < lines.Count; k++)
                {
                    valueTwo = int.Parse(lines[k]);

                    for (var j = 0; j < lines.Count; j++)
                    {
                        if (i == j)
                        {
                            break;
                        }

                        valueThree = int.Parse(lines[j]);

                        if (valueOne + valueTwo + valueThree == desiredSum)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        break;
                    }
                }

                if (found)
                {
                    break;
                }
            }

            return valueOne * valueTwo * valueThree;
        }
    }
}
