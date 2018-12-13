using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public static class Day07
    {
        public static string Result;

        public static string GetCorrectOrder(List<string> lines)
        {
            var instructions = GetInstructions(lines);
            //get list of all letters
            var distinctSteps = instructions.Select(s => s.Step).Distinct();
            var distinctDependsOn = instructions.Select(s => s.DependsOnStep).Distinct();
            var firstChar = distinctSteps.FirstOrDefault(d => !distinctDependsOn.Contains(d));

            var instructionsForLetter = instructions.Where(i => i.Step == firstChar).OrderBy(o => o.DependsOnStep).ToList();
            Result += firstChar;

            for (int i = 0; i < instructionsForLetter.Count; i++)
            {
                if (i < instructionsForLetter.Count - 1)
                {
                    ProcessStep(instructionsForLetter[i].DependsOnStep, instructions, instructionsForLetter[i + 1].DependsOnStep);
                }
                else
                {
                    ProcessStep(instructionsForLetter[i].DependsOnStep, instructions, null);
                }
            }

            return Result;
        }

        private static void ProcessStep(char initialLetter, List<Instruction> instructions, char? nextLetter)
        {
            var instructionsForLetter = instructions.Where(i => i.Step == initialLetter).OrderBy(o => o.DependsOnStep).ToList();

            Result += initialLetter;

            for (int i = 0; i < instructionsForLetter.Count; i++)
            {
                if (nextLetter.HasValue)
                {
                    if (instructionsForLetter[i].DependsOnStep < nextLetter)
                    {
                        if (i < instructionsForLetter.Count - 1)
                        {
                            ProcessStep(instructionsForLetter[i].DependsOnStep, instructions, instructionsForLetter[i + 1].DependsOnStep);
                        }
                    }
                    else
                    {
                        ProcessStep((char)nextLetter, instructions, null);
                    }
                }
            }
        }

        private static List<Instruction> GetInstructions(List<string> lines)
        {
            var result = new List<Instruction>();

            foreach (var line in lines)
            {
                var stripped = line.Replace("Step ", "").Replace(" must be finished before step ", "").Replace(" can begin.", "");

                result.Add(new Instruction{Step = stripped[0], DependsOnStep = stripped[1]});
            }

            return result;
        }

        public class Instruction
        {
            public char Step { get; set; }
            public char DependsOnStep { get; set; }
        }
    }
}
