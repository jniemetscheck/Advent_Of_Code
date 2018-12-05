using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public static class Day05
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day05.txt";

        public static int GetPart1Result()
        {
            var input = File.ReadAllText(FilePath);

            return GetNumberOfUnitsRemaining(input);
        }

        public static int GetPart2Result()
        {
            var input = File.ReadAllText(FilePath);

            return GetMinimumUnitsLength(input);
        }

        public static int GetNumberOfUnitsRemaining(string input)
        {
            var units = input.Select(x => x.ToString()).ToList();

            var length = units.Count;

            while (true)
            {
                units = GetStripped(units);

                if (length == units.Count)
                {
                    break;
                }

                length = units.Count;
            }

            return units.Count;
        }

        public static int GetMinimumUnitsLength(string input)
        {
            var units = input.Select(x => x.ToString()).ToList();
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(x => x.ToString().ToLower()).ToList();
            var minimumLength = units.Count;

            foreach (var letter in letters)
            {
                var temp = new List<string>(units);
                var length = temp.Count;
                temp.RemoveAll(r => r == letter.ToLower() || r == letter.ToUpper());

                while (true)
                {
                    temp = GetStripped(temp);

                    if (length == temp.Count)
                    {
                        break;
                    }

                    length = temp.Count;
                }

                if (minimumLength > length)
                {
                    minimumLength = length;
                }
            }

            return minimumLength;
        }

        private static List<string> GetStripped(List<string> units)
        {
            var previousCharacter = "";

            for (int i = 0; i < units.Count; i++)
            {
                if (previousCharacter.ToLower() == units[i].ToLower())
                {
                    //we have the same type at least, now check for opposite polarity
                    if ((previousCharacter.IsLowerCase() && units[i].IsUpperCase()) || (previousCharacter.IsUpperCase() && units[i].IsLowerCase()))
                    {
                        //we have opposite polarity, remove both characters
                        units.RemoveRange(i - 1, 2);
                        break;
                    }
                }

                previousCharacter = units[i];
            }

            return units;
        }

        private static bool IsLowerCase(this string text)
        {
            if (string.IsNullOrEmpty(text)) { return true; }
            foreach (char c in text)
                if (char.IsLetter(c) && !char.IsLower(c))
                    return false;

            return true;
        }

        private static bool IsUpperCase(this string text)
        {
            if (string.IsNullOrEmpty(text)) { return true; }
            foreach (char c in text)
                if (char.IsLetter(c) && !char.IsUpper(c))
                    return false;

            return true;
        }
    }
}
