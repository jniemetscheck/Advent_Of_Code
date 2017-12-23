using AdventOfCode2017.Day22;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ThisDay = AdventOfCode2017.Day22;

namespace AdventOfCode2017.Days
{
    public static class Day22
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day22.txt";

        public static long GetPart1Result()
        {
            return GetResult(GetMap(), false);
        }

        public static long GetResult(List<List<Coordinate>> map, bool isResistantVirus)
        {
            var currentRowIndex = (int)Math.Floor((decimal)(map.Count / 2));
            var currentColumnIndex = (int)Math.Floor((decimal)(map[0].Count / 2));
            var currentlyFacing = ThisDay.Direction.Up;
            var newlyInfectedCount = 0;
            var iterateTo = isResistantVirus ? 10000000 : 10000;

            for (int i = 1; i <= iterateTo; i++)
            {
                var currentNode = map[currentRowIndex][currentColumnIndex];

                if (currentNode.IsInfected)
                {
                    currentlyFacing = GetNewDirection(currentlyFacing, ThisDay.Direction.Right);
                }
                else
                {
                    currentlyFacing = GetNewDirection(currentlyFacing, ThisDay.Direction.Left);
                    newlyInfectedCount++;
                }

                currentNode.CurrentlyFacing = currentlyFacing;
                currentNode.IsInfected = !currentNode.IsInfected;

                var (NewRowIndex, NewtColumnIndex) = MoveForward(currentRowIndex, currentColumnIndex, currentlyFacing, map);

                currentRowIndex = NewRowIndex;
                currentColumnIndex = NewtColumnIndex;
            }

            return newlyInfectedCount;
        }

        private static (int NewRowIndex, int NewtColumnIndex) MoveForward(int currentRowIndex, int currentColumnIndex, ThisDay.Direction currentlyFacing, List<List<Coordinate>> map)
        {
            switch (currentlyFacing)
            {
                case ThisDay.Direction.Up:
                    if (currentRowIndex == 0)
                    {
                        //insert new row
                        var newRow = new List<Coordinate>();
                        for (int i = 0; i < map[currentRowIndex].Count; i++)
                        {
                            newRow.Add(new Coordinate());
                        }
                        map.Insert(0, newRow);
                        currentRowIndex++;
                    }
                    currentRowIndex--;
                    break;
                case ThisDay.Direction.Right:
                    if (currentColumnIndex == map[currentRowIndex].Count - 1)
                    {
                        //add new item to every row
                        foreach (var item in map)
                        {
                            item.Add(new Coordinate());
                        }
                    }
                    currentColumnIndex++;
                    break;
                case ThisDay.Direction.Down:
                    if (currentRowIndex == map.Count - 1)
                    {
                        //insert new row
                        var newRow = new List<Coordinate>();
                        for (int i = 0; i < map[currentRowIndex].Count; i++)
                        {
                            newRow.Add(new Coordinate());
                        }
                        map.Add(newRow);
                    }
                    currentRowIndex++;
                    break;
                case ThisDay.Direction.Left:
                    if (currentColumnIndex == 0)
                    {
                        //insert new item in each row
                        foreach (var item in map)
                        {
                            item.Insert(0, new Coordinate());
                        }
                        currentColumnIndex = 1;
                    }
                    currentColumnIndex--;
                    break;
                default:
                    break;
            }

            return (currentRowIndex, currentColumnIndex);
        }

        private static ThisDay.Direction GetNewDirection(ThisDay.Direction currentlyFacing, ThisDay.Direction directionToTurn)
        {
            var result = (int)currentlyFacing;
            switch (directionToTurn)
            {
                case ThisDay.Direction.Left:
                    result--;
                    break;
                case ThisDay.Direction.Right:
                    result++;
                    break;
                default:
                    break;
            }

            if (result < 1)
            {
                result = 4;
            }
            if (result > 4)
            {
                result = 1;
            }

            return (ThisDay.Direction)result;
        }

        private static List<List<Coordinate>> GetMap()
        {
            var map = new List<List<ThisDay.Coordinate>>();
            var lines = File.ReadAllLines(FilePath).ToList();

            foreach (var line in lines)
            {
                var mapItem = new List<ThisDay.Coordinate>();
                var segments = line.ToArray();

                foreach (var item in segments)
                {
                    var coordinate = new ThisDay.Coordinate();
                    coordinate.IsInfected = item == '#';

                    mapItem.Add(coordinate);
                }

                map.Add(mapItem);
            }

            return map;
        }
    }
}

namespace AdventOfCode2017.Day22
{
    public class Coordinate
    {
        public bool WasOriginallyInfected { get; set; }
        public bool IsInfected { get; set; }
        public bool IsWeakened { get; set; }
        public Direction CurrentlyFacing { get; set; }
    }

    public enum Direction
    {
        //so we can do math
        Up = 1,
        Right = 2,
        Down = 3,
        Left = 4
    }
}
