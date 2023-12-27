using AdventOfCode2023.Classes.Day08;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2023.Days
{
    public static class Day08
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day08.txt";

        public static double GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);
            var map = GetMappedMap(lines.ToList());

            return GetNumberOfSteps(map, "AAA", "ZZZ");
        }

        public static double GetResultPartTwo()
        {
            var lines = File.ReadAllLines(FilePath);
            var map = GetMappedMap(lines.ToList());

            return GetNumberOfGhostSteps(map);
        }

        public static Map GetMappedMap(List<string> lines)
        {
            var result = new Map { MapItems = new List<MapItem>() };
            var movementInstructions = new List<string>();
            var noMoreMovementInstructions = false;

            foreach (var line in lines)
            {
                if (!noMoreMovementInstructions)
                {
                    if (line == string.Empty)
                    {
                        noMoreMovementInstructions = true;
                        result.MovementInstructions = movementInstructions;
                    }
                    else
                    {
                        foreach (var item in line)
                        {
                            movementInstructions.Add(item.ToString());
                        }
                    }
                }
                else
                {
                    var item = new MapItem();
                    var lineSplit = line.Split('=');

                    item.Origin = lineSplit[0].Trim();

                    var movementSplit = lineSplit[1].Trim().Replace("(", string.Empty).Replace(")", string.Empty).Split(',');

                    item.LeftDestination = movementSplit[0].Trim();
                    item.RightDestination = movementSplit[1].Trim();

                    result.MapItems.Add(item);
                }
            }

            return result;
        }

        public static double GetNumberOfSteps(Map map, string startAt, string lookingFor)
        {
            var result = 0d;
            var processing = true;
            var currentPosition = 0;

            for (var i = 0; i < map.MapItems.Count; i++)
            {
                if (map.MapItems[i].Origin.ToLower() == startAt.ToLower())
                {
                    currentPosition = i;
                    break;
                }
            }

            while (processing)
            {
                var processResult = ProcessInstructions(map, lookingFor, currentPosition);

                processing = !processResult.ReachedDestination;
                currentPosition = processResult.CurrentPosition;
                result += processResult.NumberOfSteps;
            }

            return result;
        }

        public static double GetNumberOfGhostSteps(Map map)
        {
            var startPositions = new List<int>();
            //var lcmInput = new List<long>();

            for (var i = 0; i < map.MapItems.Count; i++)
            {
                if (map.MapItems[i].Origin.ToLower().EndsWith("a"))
                {
                    startPositions.Add(i);
                }
            }

            var result = 1d;
            var processing = true;
            var currentPosition = startPositions[0];
            while (processing)
            {
                var processResult = ProcessInstructions(map, "z", currentPosition);

                processing = !processResult.ReachedDestination;
                currentPosition = processResult.CurrentPosition;
                result += processResult.NumberOfSteps;

                //if (!processing)
                //{
                //    lcmInput.Add((long)processResult.NumberOfSteps);
                //}
            }


            return lcm(startPositions.Count, (long)result);
        }

        //static long LCM(long[] numbers)
        //{
        //    return numbers.Aggregate(lcm);
        //}
        static long lcm(long a, long b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }
        static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        public static (MapItem MapItem, int Position) GetMapItem(List<MapItem> mapItems, int position, string coordinateDirection)
        {
            var mapItem = mapItems[position];
            var nextItemIndex = 0;

            var lookFor = coordinateDirection.ToLower() == "l" ? mapItem.LeftDestination.ToLower() : mapItem.RightDestination.ToLower();

            for (var i = 0; i < mapItems.Count; i++)
            {
                if (mapItems[i].Origin.ToLower() == lookFor)
                {
                    nextItemIndex = i;
                    break;
                }
            }

            return (mapItems[nextItemIndex], nextItemIndex);
        }

        public static (bool ReachedDestination, double NumberOfSteps, int CurrentPosition) ProcessInstructions(Map map, string lookingFor, int currentPosition)
        {
            var found = false;
            var stepCount = 0;
            var lastPosition = currentPosition;

            for (var i = 0; i < map.MovementInstructions.Count; i++)
            {
                var thisElement = map.MapItems[lastPosition];

                if (thisElement.Origin == lookingFor)
                {
                    found = true;
                    break;
                }

                var newLookingFor = map.MovementInstructions[i].ToLower() == "l" ? thisElement.LeftDestination : thisElement.RightDestination;

                if (newLookingFor.ToLower().EndsWith(lookingFor.ToLower()))
                {
                    found = true;
                    stepCount++;
                    break;
                }

                for (var j = 0; j < map.MapItems.Count; j++)
                {
                    if (newLookingFor.ToLower().EndsWith(map.MapItems[j].Origin.ToLower()))
                    {
                        lastPosition = j;
                        Console.WriteLine(lastPosition);
                        stepCount++;
                        break;
                    }
                }
            }

            return (found, stepCount, lastPosition);
        }
    }
}