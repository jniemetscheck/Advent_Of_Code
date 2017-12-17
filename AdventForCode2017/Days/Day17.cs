using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public static class Day17
    {
        public static int GetPart1Result()
        {
            var values = GetPart1Result(314);

            return values[values.IndexOf(values.FirstOrDefault(x => x == 2017)) + 1];
        }

        public static List<int> GetPart1Result(int spinAmount)
        {
            var values = new List<int> { 0 };
            var currentPosition = 0;
            var insertionCount = 0;

            for (int i = 1; i <= 2017; i++)
            {
                //spin through values spinAmount
                for (int j = 1; j <= spinAmount; j++)
                {
                    if (currentPosition + j > values.Count)
                    {
                        currentPosition = 0;
                    }
                    else
                    {
                        currentPosition++;
                    }
                }

                currentPosition++;
                values.Insert(currentPosition, i);
                insertionCount++;
            }

            return values;
        }
    }
}
