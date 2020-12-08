using System.Collections.Generic;
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

            return GetAccumulator(instructions).Accumulator;
        }

        public static int GetResultPartTwo()
        {
            var input = File.ReadAllLines(FilePath);

            var instructions = GetMappedInstructions(input.ToList());

            return FindIncorrectOperation(instructions);
        }

        public static int FindIncorrectOperation(List<Instruction> instructions)
        {
            var accumulator = 0;

            for (var i = 0; i < instructions.Count; i++)
            {
                var currentInstruction = new Instruction
                {
                    Operation = instructions[i].Operation, 
                    Plus = instructions[i].Plus, 
                    Value = instructions[i].Value
                };

                if (currentInstruction.Operation == Operation.None)
                {
                    instructions[i].Operation = Operation.Jump;
                }else if (currentInstruction.Operation == Operation.Jump)
                {
                    instructions[i].Operation = Operation.None;
                }

                foreach (var instruction in instructions)
                {
                    instruction.HasBeenRun = false;
                }
                var result = GetAccumulator(instructions);

                if (result.TerminatedNormally)
                {
                    accumulator = result.Accumulator;
                    break;
                }

                instructions[i] = currentInstruction;
            }

            return accumulator;
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

        public static (int Accumulator, bool TerminatedNormally) GetAccumulator(List<Instruction> instructions)
        {
            var accumulator = 0;
            var currentStepIndex = 0;
            var infiniteLoop = false;

            while (true)
            {
                if (currentStepIndex >= instructions.Count)
                {
                    break;
                }
                var currentStep = instructions[currentStepIndex];

                if (currentStep.HasBeenRun)
                {
                    infiniteLoop = true;
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

            return (accumulator, !infiniteLoop);
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
