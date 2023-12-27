using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2023.Classes.Day09;

namespace AdventOfCode2023.Days
{
    public static class Day09
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day09.txt";

        public static double GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);
            var historyLines = GetMappedInput(lines.ToList());

            return GetNextValueNumberSum(historyLines.ToList());
        }

        public static double GetResultPartTwo()
        {
            var lines = File.ReadAllLines(FilePath);
            var historyLines = GetMappedInput(lines.ToList());

            return GetNextValueNumberSumReverse(historyLines.ToList());
        }

        private static IEnumerable<HistoryItem> GetMappedInput(List<string> lines)
        {
            var result = new List<HistoryItem>();

            foreach (var line in lines)
            {
                var historyItem = new HistoryItem { Numbers = new List<double>() };

                foreach (var inputItem in line.Split(' '))
                {
                    historyItem.Numbers.Add(double.Parse(inputItem));
                }

                result.Add(historyItem);
            }

            return result;
        }

        private static double GetNextValueNumberSum(List<HistoryItem> historyItems)
        {
            var result = 0d;

            foreach (var historyItem in historyItems)
            {
                var done = false;
                var finalList = new List<List<double>>();
                var next = historyItem.Numbers;
                finalList.Add(next);
                while (!done)
                {
                    next = GetNextDifferenceLine(next);

                    if (next.TrueForAll(m => m == 0))
                    {
                        done = true;
                    }
                    else
                    {
                        finalList.Add(next);
                    }
                }

                var missingNumber = 0d;
                var incremeter = finalList.LastOrDefault().LastOrDefault();
                for (var i = finalList.Count - 2; i >= 0; i--)
                {
                    missingNumber = incremeter + finalList[i].LastOrDefault();
                    incremeter = missingNumber;
                }

                result += missingNumber;
            }

            return result;
        }

        private static double GetNextValueNumberSumReverse(List<HistoryItem> historyItems)
        {
            var result = 0d;

            foreach (var historyItem in historyItems)
            {
                var done = false;
                var finalList = new List<List<double>>();
                var next = historyItem.Numbers;
                finalList.Add(next);
                while (!done)
                {
                    next = GetNextDifferenceLine(next);

                    if (next.TrueForAll(m => m == 0))
                    {
                        done = true;
                    }
                    else
                    {
                        finalList.Add(next);
                    }
                }

                var missingNumber = 0d;
                var incremeter = finalList.LastOrDefault().FirstOrDefault();
                for (var i = finalList.Count - 2; i >= 0; i--)
                {
                    missingNumber = finalList[i].FirstOrDefault() - incremeter;
                    incremeter = missingNumber;
                }

                result += missingNumber;
            }

            return result;
        }

        private static List<double> GetNextDifferenceLine(List<double> line)
        {
            var result = new List<double>();

            for (var i = 0; i < line.Count - 1; i++)
            {
                result.Add(line[i + 1] - line[i]);
            }

            return result;
        }
    }
}