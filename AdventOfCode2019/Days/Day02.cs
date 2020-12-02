using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Days
{
    public static class Day02
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day02.txt";

        public static int GetResultPartOne()
        {
            var input = File.ReadAllText(FilePath);

            return GetPositionWhenProgramHalts(input.Split(',').ToList());
        }

        //public static int GetResultPartTwo()
        //{
        //    var lines = File.ReadAllLines(FilePath);

        //    return GetTotalFuelRequirements(lines.ToList());
        //}

        public static int GetPositionWhenProgramHalts(List<string> inputs)
        {
            for (int position = 0; position < inputs.Count; position++)
            {
                var opCode = int.Parse(inputs[position]);

                if (opCode == 1)
                {
                    //inputs[int.Parse(inputs[])];
                }
            }

            return 0;
        }
    }
}
