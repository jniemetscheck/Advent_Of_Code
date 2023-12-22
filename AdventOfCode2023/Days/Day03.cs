using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;

namespace AdventOfCode2023.Days
{
    public static class Day03
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day03.txt";

        public static double GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);
            var engine = GetMappedEngine(lines.ToList());

            return GetMissingEnginePart(engine);
        }

        //public static double GetResultPartTwo()
        //{
        //    var lines = File.ReadAllLines(FilePath);
        //    var games = GetMappedGames(lines.ToList());

        //    return GetMinimumCubePowerSum(games);
        //}

        public static Engine GetMappedEngine(List<string> input)
        {
            var engine = new Engine { RegularParts = new List<RegularPart>(), SpecialParts = new List<SpecialPart>() };

            for (var j = 0; j < input.Count; j++)
            {
                var currentNumber = string.Empty;

                for (var i = 0; i < input[j].Length; i++)
                {
                    //ignore periods
                    if (input[j][i] == '.')
                    {
                        if (currentNumber.Length > 0)
                        {
                            //save the current number as we've reached the end of the number
                            var regularEnginePart = new RegularPart { Number = int.Parse(currentNumber), Y = j };

                            regularEnginePart.StartX = i - currentNumber.Length;
                            regularEnginePart.EndX = i - 1;

                            engine.RegularParts.Add(regularEnginePart);

                            currentNumber = string.Empty;
                        }
                        continue;
                    }

                    if (char.IsDigit(input[j][i]))
                    {
                        //part of regular number
                        currentNumber += input[j][i];

                        if (i == input[j].Length - 1)
                        {
                            //we're at the EOL, add number
                            var regularEnginePart = new RegularPart { Number = int.Parse(currentNumber), Y = j };

                            regularEnginePart.StartX = i - currentNumber.Length;
                            regularEnginePart.EndX = i - 1;

                            engine.RegularParts.Add(regularEnginePart);

                            currentNumber = string.Empty;
                        }
                    }
                    else
                    {
                        //special character
                        engine.SpecialParts.Add(new SpecialPart { X = i, Y = j });

                        if (currentNumber.Length > 0)
                        {
                            //save the current number as we've reached the end of the number
                            var regularEnginePart = new RegularPart { Number = int.Parse(currentNumber), Y = j };

                            regularEnginePart.StartX = i - currentNumber.Length;
                            regularEnginePart.EndX = i - 1;

                            engine.RegularParts.Add(regularEnginePart);

                            currentNumber = string.Empty;
                        }
                    }
                }
            }

            return engine;
        }

        public static double GetMissingEnginePart(Engine engine)
        {
            var result = 0d;

            foreach (var regularPart in engine.RegularParts)
            {
                var isAdjacent = false;

                foreach (var specialPart in engine.SpecialParts)
                {
                    if (regularPart.Y == specialPart.Y || regularPart.Y == specialPart.Y - 1 || regularPart.Y == specialPart.Y + 1)
                    {
                        //we have a possible correct Y part, check X
                        if (specialPart.X >= regularPart.StartX - 1 && specialPart.X <= regularPart.EndX + 1)
                        {
                            isAdjacent = true;
                            break;
                        }
                    }
                }

                if (isAdjacent)
                {
                    result += regularPart.Number;
                }
            }

            return result;
        }
    }

    public class Engine
    {
        public List<RegularPart> RegularParts { get; set; }
        public List<SpecialPart> SpecialParts { get; set; }
    }

    public class RegularPart
    {
        public int StartX { get; set; }
        public int EndX { get; set; }
        public int Y { get; set; }
        public int Number { get; set; }
    }

    public class SpecialPart
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}