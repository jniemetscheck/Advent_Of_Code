using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace AdventOfCode2023.Days
{
    public static class Day01
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day01.txt";

        public static int GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetSumOfCalibrationNumbers(lines.ToList());
        }

        public static int GetResultPartTwo()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetSumOfCalibrationNumbersAndStrings(lines.ToList());
        }

        private static readonly Dictionary<int, string> _wordNumbers = new Dictionary<int, string>()
        {
            { 1, "one" },
            { 2, "two" },
            { 3, "three" },
            { 4, "four" },
            { 5, "five" },
            { 6, "six" },
            { 7, "seven" },
            { 8, "eight" },
            { 9, "nine" }
        };

        private static int GetSumOfCalibrationNumbersAndStrings(List<string> lines)
        {
            var total = 0;
            foreach (var line in lines)
            {
                var indices = new Dictionary<int, int>();

                //check for numbers
                for (var i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                    {
                        indices.Add(i, int.Parse(line[i].ToString()));
                    }
                }

                //check for words
                foreach (var wordNumber in _wordNumbers)
                {
                    if (line.Contains(wordNumber.Value))
                    {
                        var firstIndex = GetIndexOfNumberWord(line, wordNumber.Value).Value;
                        var lastIndex = GetIndexOfNumberWord(line, wordNumber.Value, false).Value;

                        indices.Add(firstIndex, wordNumber.Key);

                        if (lastIndex != firstIndex)
                        {
                            indices.Add(lastIndex, wordNumber.Key);
                        }
                    }
                }

                //do total
                if (indices.Count == 1)
                {
                    total += int.Parse((indices.First().Value.ToString()) + (indices.First().Value.ToString()));
                }
                else
                {
                    total += int.Parse((indices.OrderBy(o => o.Key).First().Value.ToString()) + (indices.OrderBy(o => o.Key).Last().Value.ToString()));
                }
            }
            
            return total;
        }

        private static int GetSumOfCalibrationNumbers(List<string> lines)
        {
            var total = 0;

            for (var i = 0; i < lines.Count; i++)
            {
                var numericCharacters = new List<string>();

                foreach (var character in lines[i])
                {
                    if (char.IsDigit(character))
                    {
                        numericCharacters.Add(character.ToString());
                    }
                }

                total += int.Parse(numericCharacters.First() + numericCharacters.Last());
            }

            return total;
        }



        private static int? GetIndexOfNumberWord(string textToCheck, string word, bool getFirstIndex = true)
        {
            int? index = null;

            if (textToCheck.ToLower().Contains(word))
            {
                index = getFirstIndex ? textToCheck.ToLower().IndexOf(word) : textToCheck.ToLower().LastIndexOf(word);
            }

            return index;
        }
    }
}