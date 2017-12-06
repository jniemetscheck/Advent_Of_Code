using System.Collections.Generic;
using System.IO;

namespace AdventForCode2017.Days
{
    public static class Day02
    {
        public static int GetPart1Result()
        {
            var checkSum = 0;

            foreach (var segmentedLine in GetSegmentedLines())
            {
                var max = 0;
                var min = 10000;
                for (int i = 0; i < segmentedLine.Length; i++)
                {
                    var number = int.Parse(segmentedLine[i].ToString());
                    max = number > max ? number : max;
                    min = number < min ? number : min;
                }

                checkSum += (max - min);
            }

            return checkSum;
        }

        public static int GetPart2Result()
        {
            var checkSum = 0;

            foreach (var segmentedLine in GetSegmentedLines())
            {
                var lineTotal = 0;
                for (int i = 0; i < segmentedLine.Length; i++)
                {
                    for (int j = i + 1; j < segmentedLine.Length; j++)
                    {
                        var numberOne = int.Parse(segmentedLine[i]);
                        var numberTwo = int.Parse(segmentedLine[j]);

                        if (numberOne % numberTwo == 0)
                        {
                            lineTotal += numberOne / numberTwo;
                        }
                        else if (numberTwo % numberOne == 0)
                        {
                            lineTotal += numberTwo / numberOne;
                        }
                    }
                }

                checkSum += lineTotal;
            }

            return checkSum;
        }

        private static List<string[]> GetSegmentedLines()
        {
            var result = new List<string[]>();
            StreamReader file = new System.IO.StreamReader(Directory.GetCurrentDirectory() + @"/Input/Day02.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {
                result.Add(line.Split('\t'));
            }

            return result;
        }
    }
}
