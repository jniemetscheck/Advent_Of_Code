using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2023.Classes.Day10;

namespace AdventOfCode2023.Days
{
    public static class Day10
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day10.txt";

        public static double GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);
            var fullMap = GetFullMap(lines.ToList());

            return GetFarthestAway(fullMap, FromDirection.South, 0, -1);
        }

        //public static double GetResultPartTwo()
        //{
        //    var lines = File.ReadAllLines(FilePath);
        //    var historyLines = GetMappedInput(lines.ToList());

        //    return GetNextValueNumberSumReverse(historyLines.ToList());
        //}

        public static List<List<string>> GetFullMap(List<string> lines)
        {
            return lines.Select(line => line.Select(pipeValue => pipeValue.ToString()).ToList()).ToList();
        }

        public static double GetFarthestAway(List<List<string>> fullMap, FromDirection fromDirection, int xAway, int yAway)
        {
            var count = 0;
            var startingCoordinates = GetStartingCoordinates(fullMap);
            var nextX = startingCoordinates.X + xAway;
            var nextY = startingCoordinates.Y + yAway;
            var nextFromDirection = fromDirection;

            do
            {
                var nextCoordinates = GetNextCoordinateIncrementers(nextFromDirection, fullMap[nextY][nextX]);
                nextX += nextCoordinates.X;
                nextY += nextCoordinates.Y;
                nextFromDirection = nextCoordinates.FromDirection;
                count++;
            } while (startingCoordinates.X != nextX || startingCoordinates.Y != nextY);


            return count % 2 == 0 ? count % 2 : count / 2 + 1;
        }

        public static (int X, int Y) GetStartingCoordinates(List<List<string>> fullMap)
        {
            var startX = 0;
            var startY = 0;

            for (var i = 0; i < fullMap.Count; i++)
            {
                for (var j = 0; j < fullMap[i].Count; j++)
                {
                    if (fullMap[i][j].ToLower() == "s")
                    {
                        startY = i;
                        startX = j;
                        break;
                    }
                }
            }

            return (startX, startY);
        }

        public static (int X, int Y, FromDirection FromDirection) GetNextCoordinateIncrementers(FromDirection fromDirection, string currentCharacter)
        {
            var nextX = 0;
            var nextY = 0;
            var nextFromDirection = fromDirection;

            switch (currentCharacter)
            {
                case "|":
                    if (fromDirection == FromDirection.North)
                    {
                        nextY++;
                    }
                    else if (fromDirection == FromDirection.South)
                    {
                        nextY--;
                    }
                    break;
                case "-":
                    if (fromDirection == FromDirection.West)
                    {
                        nextX++;
                    }
                    else if (fromDirection == FromDirection.East)
                    {
                        nextX--;
                    }
                    break;
                case "L":
                    if (fromDirection == FromDirection.North)
                    {
                        nextX++;
                    }
                    else if (fromDirection == FromDirection.East)
                    {
                        nextY--;
                    }
                    break;
                case "J":
                    if (fromDirection == FromDirection.North)
                    {
                        nextX--;
                    }
                    else if (fromDirection == FromDirection.West)
                    {
                        nextY--;
                    }
                    break;
                case "7":
                    if (fromDirection == FromDirection.South)
                    {
                        nextX--;
                    }
                    else if (fromDirection == FromDirection.West)
                    {
                        nextY++;
                    }
                    break;
                case "F":
                    if (fromDirection == FromDirection.South)
                    {
                        nextX++;
                    }else if (fromDirection == FromDirection.East)
                    {
                        nextY++;
                    }
                    break;
            }

            if (nextX > 0)
            {
                nextFromDirection = FromDirection.West;
            }

            if (nextX < 0)
            {
                nextFromDirection = FromDirection.East;
            }

            if (nextY > 0)
            {
                nextFromDirection = FromDirection.North;
            }

            if (nextY < 0)
            {
                nextFromDirection = FromDirection.South;
            }

            return (nextX, nextY, nextFromDirection);
        }
    }
}