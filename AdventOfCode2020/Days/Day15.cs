using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day15
    {
        public static int GetResultPartOne()
        {
            return GetNumber(2020, new List<int> { 1, 0, 18, 10, 19, 6 });
        }

        public static int GetResultPartTwo()
        {
            return GetNumber(2020, new List<int> { 1, 0, 18, 10, 19, 6 });
        }

        public static int GetNumber(int numberPlace, List<int> startingNumbers)
        {
            var memory = new Dictionary<int, List<int>>();

            var position = 1;
            foreach (var startingNumber in startingNumbers)
            {
                memory.Add(startingNumber, new List<int> { position });
                position++;
            }

            var lastNumberSpoken = 0;
            for (var i = startingNumbers.Count + 1; i <= numberPlace; i++)
            {
                lastNumberSpoken = SpeakNumber(memory, i -  1);

                if (memory.ContainsKey(lastNumberSpoken))
                {
                    memory[lastNumberSpoken].Add(i);
                }
                else
                {
                    memory.Add(lastNumberSpoken, new List<int> { i });
                }

                //LogMemory(memory, i);
            }

            return lastNumberSpoken;
        }


        public static int SpeakNumber(Dictionary<int, List<int>> memory, int lastPosition)
        {
            var numberSpoken = 0;

            foreach (var memoryKey in memory)
            {
                if (memoryKey.Value.Contains(lastPosition))
                {
                    if (memoryKey.Value.Count == 1)
                    {
                        //first time
                        numberSpoken = 0;
                        break;
                    }
                    else
                    {
                        //get distance
                        numberSpoken = memoryKey.Value[memoryKey.Value.Count - 1] - memoryKey.Value[memoryKey.Value.Count - 2];
                        break;
                    }
                }
            }

            return numberSpoken;
        }

        public static void LogMemory(Dictionary<int, List<int>> memory, int position)
        {
            for (var i = 1; i <= position; i++)
            {
                foreach (var item in memory)
                {
                    if (item.Value.Contains(i))
                    {
                        Console.WriteLine(item.Key);
                        break;
                    }
                }
            }

            Console.WriteLine();
        }
    }
}
