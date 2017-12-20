using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public static class Day19
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day19.txt";

        public static string GetPart1Result()
        {
            return GetResult(GetMap()).Path;
        }

        public static int GetPart2Result()
        {
            return GetResult(GetMap()).Steps;
        }

        public static (string Path, int Steps) GetResult(List<List<string>> map)
        {
            var path = string.Empty;
            var numberOfSteps = 1;
            var travelDirection = TravelDirection.Down;
            var currentY = 0;
            var currentX = map[currentY].IndexOf("|");

            currentY++;
            while (true)
            {
                var currentChar = map[currentY][currentX];
                switch (currentChar)
                {
                    case "|":
                        if (travelDirection == TravelDirection.Left || travelDirection == TravelDirection.Right)
                        {
                            if (travelDirection == TravelDirection.Left)
                            {
                                currentX--;
                            }
                            else
                            {
                                currentX++;
                            }
                        }
                        else if (travelDirection == TravelDirection.Down)
                        {
                            currentY++;
                        }
                        else
                        {
                            currentY--;
                        }
                        break;
                    case "-":
                        if (travelDirection == TravelDirection.Up || travelDirection == TravelDirection.Down)
                        {
                            if (travelDirection == TravelDirection.Up)
                            {
                                currentY--;
                            }
                            else
                            {
                                currentY++;
                            }
                        }
                        else if (travelDirection == TravelDirection.Left)
                        {
                            currentX--;
                        }
                        else
                        {
                            currentX++;
                        }
                        break;
                    case "+":
                        if (travelDirection == TravelDirection.Down || travelDirection == TravelDirection.Up)
                        {
                            if (currentX == 0)
                            {
                                travelDirection = TravelDirection.Right;
                                currentX++;
                            }
                            else if (currentX == map[currentY].Count - 1)
                            {
                                travelDirection = TravelDirection.Left;
                                currentX--;
                            }
                            else
                            {
                                if (map[currentY][currentX - 1] != " ")
                                {
                                    travelDirection = TravelDirection.Left;
                                    currentX--;
                                }
                                else
                                {
                                    travelDirection = TravelDirection.Right;
                                    currentX++;
                                }
                            }
                        }
                        else if (travelDirection == TravelDirection.Left || travelDirection == TravelDirection.Right)
                        {
                            if (currentY == 0)
                            {
                                travelDirection = TravelDirection.Down;
                                currentY++;
                            }
                            else if (currentY == map.Count - 1)
                            {
                                travelDirection = TravelDirection.Up;
                                currentY--;
                            }
                            else
                            {
                                if (map[currentY + 1][currentX] != " ")
                                {
                                    travelDirection = TravelDirection.Down;
                                    currentY++;
                                }
                                else
                                {
                                    travelDirection = TravelDirection.Up;
                                    currentY--;
                                }
                            }
                        }
                        else
                        {
                            if (currentY == 0)
                            {
                                currentY++;
                            }
                            else if (currentY == map.Count - 1)
                            {
                                currentY--;
                            }
                            else
                            {
                                if (map[currentY + 1][currentX] != " ")
                                {
                                    travelDirection = TravelDirection.Down;
                                }
                                else
                                {
                                    travelDirection = TravelDirection.Up;
                                }
                            }
                        }
                        break;
                    case " ":
                        currentX = -1;
                        numberOfSteps--;
                        break;
                    default:
                        //  letter, add to list
                        path += currentChar;
                        if (travelDirection == TravelDirection.Down)
                        {
                            currentY++;
                        }
                        else if (travelDirection == TravelDirection.Up)
                        {
                            currentY--;
                        }
                        else if (travelDirection == TravelDirection.Left)
                        {
                            currentX--;
                        }
                        else
                        {
                            currentX++;
                        }
                        break;
                }

                numberOfSteps++;

                if (currentX < 0 || currentY < 0 || currentY >= map.Count || currentX >= map[currentY].Count)
                {
                    break;
                }
            }

            return (path, numberOfSteps);
        }

        private static List<List<string>> GetMap()
        {
            var map = new List<List<string>>();
            var lines = File.ReadAllLines(FilePath).ToList();

            foreach (var line in lines)
            {
                map.Add(line.Select(c => c.ToString()).ToList());
            }

            return map;
        }
    }

    public enum TravelDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
