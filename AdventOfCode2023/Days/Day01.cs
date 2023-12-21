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

        private static int GetSumOfCalibrationNumbersAndStrings(List<string> lines)
        {
            var total = 0;
            foreach (var line in lines)
            {
                var indices = new Dictionary<int, string>();

                //check for numbers
                for (var i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                    {
                        indices.Add(i, line[i].ToString());
                    }
                }

                //check for words
                if (GetIndexOfNumberWord(line, "one").HasValue)
                {
                    indices.Add(GetIndexOfNumberWord(line, "one").Value, 1.ToString());
                }
                if (GetIndexOfNumberWord(line, "two").HasValue)
                {
                    indices.Add(GetIndexOfNumberWord(line, "two").Value, 2.ToString());
                }
                if (GetIndexOfNumberWord(line, "three").HasValue)
                {
                    indices.Add(GetIndexOfNumberWord(line, "three").Value, 3.ToString());
                }
                if (GetIndexOfNumberWord(line, "four").HasValue)
                {
                    indices.Add(GetIndexOfNumberWord(line, "four").Value, 4.ToString());
                }
                if (GetIndexOfNumberWord(line, "five").HasValue)
                {
                    indices.Add(GetIndexOfNumberWord(line, "five").Value, 5.ToString());
                }
                if (GetIndexOfNumberWord(line, "six").HasValue)
                {
                    indices.Add(GetIndexOfNumberWord(line, "six").Value, 6.ToString());
                }
                if (GetIndexOfNumberWord(line, "seven").HasValue)
                {
                    indices.Add(GetIndexOfNumberWord(line, "seven").Value, 7.ToString());
                }
                if (GetIndexOfNumberWord(line, "eight").HasValue)
                {
                    indices.Add(GetIndexOfNumberWord(line, "eight").Value, 8.ToString());
                }
                if (GetIndexOfNumberWord(line, "nine").HasValue)
                {
                    indices.Add(GetIndexOfNumberWord(line, "nine").Value, 9.ToString());
                }

                //do total
                if (indices.Count == 1)
                {
                    total += int.Parse(indices.First().Value + indices.First().Value);
                }
                else
                {
                    total += int.Parse(indices.OrderBy(o => o.Key).First().Value.ToString() + indices.OrderBy(o => o.Key).Last().Value.ToString());
                }

                Console.WriteLine(total);
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



        private static int? GetIndexOfNumberWord(string textToCheck, string word)
        {
            int? index = null;

            if (textToCheck.ToLower().Contains(word))
            {
                index = textToCheck.ToLower().IndexOf(word);
            }

            return index;
        }
    }
}