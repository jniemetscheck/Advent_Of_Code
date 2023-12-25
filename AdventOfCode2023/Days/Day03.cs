using AdventOfCode2023.Classes.Day03;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public static double GetResultPartTwo()
        {
            var lines = File.ReadAllLines(FilePath);
            var engine = GetMappedEngine(lines.ToList());

            return GetGearRatioSum(engine);
        }

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
                        engine.SpecialParts.Add(new SpecialPart { X = i, Y = j, IsPossibleGear = input[j][i] == '*' });

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

        public static double GetGearRatioSum(Engine engine)
        {
            var result = 0d;

            foreach (var specialPart in engine.SpecialParts.Where(p => p.IsPossibleGear))
            {
                var adjacentParts = new List<int>();

                foreach (var regularPart in engine.RegularParts)
                {
                    if (regularPart.Y == specialPart.Y || regularPart.Y == specialPart.Y - 1 || regularPart.Y == specialPart.Y + 1)
                    {
                        //we have a possible correct Y part, check X
                        if (specialPart.X >= regularPart.StartX - 1 && specialPart.X <= regularPart.EndX + 1)
                        {
                            adjacentParts.Add(regularPart.Number);
                        }
                    }
                }

                if (adjacentParts.Count == 2)
                {
                    result += adjacentParts[0] * adjacentParts[1];
                }
            }

            return result;
        }
    }
}