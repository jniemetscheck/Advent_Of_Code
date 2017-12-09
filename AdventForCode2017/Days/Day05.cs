using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2017.Days
{
    public static class Day05
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day05.txt";

        public static int GetPart1Result()
        {
            var input = File.ReadAllText(FilePath);

            var instructions = GetInstructions();
            var currentStep = 0;
            var totalNumberOfSteps = 0;

            while (true)
            {
                var currentInstruction = instructions[currentStep];
                instructions[currentStep]++;
                currentStep += currentInstruction;
                totalNumberOfSteps++;

                if (currentStep >= instructions.Count)
                {
                    break;
                }
            }

            return totalNumberOfSteps;
        }

        public static int GetPart2Result()
        {
            var input = File.ReadAllText(FilePath);

            var instructions = GetInstructions();
            var currentStep = 0;
            var totalNumberOfSteps = 0;

            while (true)
            {
                var currentInstruction = instructions[currentStep];

                if (currentInstruction >= 3)
                {
                    instructions[currentStep]--;
                }
                else{
                    instructions[currentStep]++;
                }
                currentStep += currentInstruction;
                totalNumberOfSteps++;

                if (currentStep >= instructions.Count)
                {
                    break;
                }
            }

            return totalNumberOfSteps;
        }

        private static List<int> GetInstructions()
        {
            var result = new List<int>();
            StreamReader file = new System.IO.StreamReader(FilePath);
            string line;

            while ((line = file.ReadLine()) != null)
            {
                result.Add(int.Parse(line));
            }

            return result;
        }
    }
}
