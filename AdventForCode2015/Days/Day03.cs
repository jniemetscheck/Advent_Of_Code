using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2015.Days
{
    public static class Day03
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day03.txt";

        public static int GetPart1Result()
        {
            var input = File.ReadAllText(FilePath).Select(c => c.ToString()).ToList();
            var grid = new List<List<bool>> { new List<bool> { true } }; //set up so that starting point has been delivered
            var currentYCoordinate = 0;
            var currentXCoordinate = 0;

            foreach (var direction in input)
            {
                switch (direction)
                {
                    case ">": //right
                        grid[currentYCoordinate].Add(true);
                        currentXCoordinate++;
                        break;
                    case "<": //left
                        if (currentXCoordinate == 0)
                        {
                            //need to insert at beginning and leave x coordinate
                            grid[currentYCoordinate].Insert(0, true);
                        }
                        else
                        {
                            currentXCoordinate--;
                            
                        }
                        break;
                    case "^": //up
                        if (currentYCoordinate == 0)
                        {
                            //need to insert new array at beginning of grid
                            grid.Insert(0, new List<bool> { true });
                        }
                        break;
                    case "v": //down
                        break;
                    default:
                        break;
                }
            }

            return 0;
            //return numberOfUniqueHousesDeliveredTo;
        }
    }
}
