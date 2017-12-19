using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public static class Day11
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day11.txt";

        public static int GetPart1Result()
        {
            var input = File.ReadAllText(FilePath);

            return GetResult(input.Split(',').ToList());
        }

        public static int GetResult(List<string> directions)
        {
            var previousDirection = string.Empty;
            var stepsAway = 0;
            foreach (var direction in directions)
            {
                if (previousDirection == string.Empty)
                {
                    stepsAway++;
                }
                else
                {
                    switch (previousDirection)
                    {
                        case "n":
                            switch (direction)
                            {
                                case "n":
                                case "nw":
                                case "ne":
                                    stepsAway++;
                                    break;
                                case "s":
                                case "sw":
                                case "se":
                                    stepsAway--;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "nw":
                            switch (direction)
                            {
                                case "n":
                                case "nw":
                                case "ne":
                                    stepsAway++;
                                    break;
                                case "s":
                                case "sw":
                                case "se":
                                    stepsAway--;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "ne":
                            break;
                        case "s":
                            break;
                        case "sw":
                            break;
                        case "se":
                            break;
                        default:
                            break;
                    }
                }

                previousDirection = direction;
            }

            return 0;
        }
    }
}
