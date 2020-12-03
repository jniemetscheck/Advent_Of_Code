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
            var position = 0;
            var value = -1;

            while (true)
            {
                if (inputs[position] == "99")
                {
                    value = int.Parse(inputs[0]);
                    break;
                }
                var instruction = new Instruction
                {
                    OpCode = int.Parse(inputs[position]),
                    InputOnePosition = int.Parse(inputs[position + 1]),
                    InputTwoPosition = int.Parse(inputs[position + 2]),
                    OutputPosition = int.Parse(inputs[position + 3])
                };

                if (instruction.OpCode == 1)
                {
                    inputs[instruction.OutputPosition] = (int.Parse(inputs[instruction.InputOnePosition]) + int.Parse(inputs[instruction.InputTwoPosition])).ToString();
                }
                else if (instruction.OpCode == 2)
                {
                    inputs[instruction.OutputPosition] = (int.Parse(inputs[instruction.InputOnePosition]) * int.Parse(inputs[instruction.InputTwoPosition])).ToString();
                }


                position += 4;
            }

            return value;
        }
    }

    public class Instruction
    {
        public int OpCode { get; set; }
        public int InputOnePosition { get; set; }
        public int InputTwoPosition { get; set; }
        public int OutputPosition { get; set; }
    }
}
