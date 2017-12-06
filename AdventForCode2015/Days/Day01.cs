using System.IO;
using System.Linq;

namespace AdventForCode2015.Days
{
    public class Day01
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day01.txt";

        public static int GetPart1Result()
        {
            var input = File.ReadAllText(FilePath).Select(c => c.ToString()).ToList();

            return input.Where(x => x == "(").Count() - input.Where(x => x == ")").Count(); 
        }

        public static int GetPart2Result()
        {
            var input = File.ReadAllText(FilePath).Select(c => c.ToString()).ToList();
            var positionWhenInBasement = 0;
            var currentFloor = 0;
            for (int i = 0; i < input.Count(); i++)
            {
                currentFloor = input[i] == "(" ? currentFloor + 1 : currentFloor - 1;
                if (currentFloor < 0)
                {
                    positionWhenInBasement = i + 1;
                    break;
                }
            }

            return positionWhenInBasement;
        }
    }
}
