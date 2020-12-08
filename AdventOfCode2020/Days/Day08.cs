using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day08
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day08.txt";

        public static int GetResultPartOne()
        {
            var input = File.ReadAllLines(FilePath);

            var instructions = GetMappedInstructions(input.ToList());

            return GetAccumulatorBeforeInfiniteLoop(instructions);
        }

        public static List<Instruction> GetMappedInstructions(List<string> input)
        {
            var result = new List<Instruction>();

            foreach (var line in input)
            {
                var instruction = new Instruction();

                var spaceSplit = line.Split(" ".ToCharArray());

                switch (spaceSplit[0])
                {
                    case "nop":
                        instruction.Operation = Operation.None;
                        break;
                    case "acc":
                        instruction.Operation = Operation.Accumulate;
                        break;
                    case "jmp":
                        instruction.Operation = Operation.Jump;
                        break;
                    default:
                        instruction.Operation = Operation.Unknown;
                        break;
                }

                if (spaceSplit[1].Substring(0, 1) == "+")
                {
                    instruction.Plus = true;
                }

                instruction.Value = int.Parse(spaceSplit[1].Substring(1, spaceSplit[1].Length - 1));

                result.Add(instruction);
            }

            return result;
        }

        public static int GetAccumulatorBeforeInfiniteLoop(List<Instruction> instructions)
        {
            var accumulator = 0;
            var currentStepIndex = 0;

            while (true)
            {
                var currentStep = instructions[currentStepIndex];

                if (currentStep.HasBeenRun)
                {
                    break;
                }

                if (currentStep.Operation == Operation.Accumulate)
                {
                    accumulator = currentStep.Plus ? accumulator + currentStep.Value : accumulator - currentStep.Value;
                    currentStepIndex++;
                }
                else if (currentStep.Operation == Operation.Jump)
                {
                    currentStepIndex = currentStep.Plus ? currentStepIndex + currentStep.Value : currentStepIndex - currentStep.Value;
                }
                else
                {
                    currentStepIndex++;
                }

                currentStep.HasBeenRun = true;
            }

            return accumulator;
        }
    }

    public class Instruction
    {
        public Operation Operation { get; set; }
        public bool Plus { get; set; }
        public int Value { get; set; }
        public bool HasBeenRun { get; set; }
    }

    public enum Operation
    {
        Accumulate,
        Jump,
        None,
        Unknown
    }
}
