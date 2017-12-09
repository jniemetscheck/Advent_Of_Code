using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days
{
    public static class Day08
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day08.txt";

        public static int GetPart1Result()
        {
            var instructions = GetInstructions();
            var known = new Dictionary<string, int>();

            foreach (var instruction in instructions)
            {
                //initialize
                if (!known.ContainsKey(instruction.Id))
                {
                    known.Add(instruction.Id, 0);
                }
                if (!known.ContainsKey(instruction.Expression.First))
                {
                    known.Add(instruction.Expression.First, 0);
                }

                //compute
                switch (instruction.Expression.CompareOperation)
                {
                    case CompareOperation.LessThan:
                        if (known[instruction.Expression.First] < instruction.Expression.Second)
                        {
                            known[instruction.Id] = instruction.Direction == Direction.Decrease ? known[instruction.Id] - instruction.AmountBy : known[instruction.Id] + instruction.AmountBy;
                        }
                        break;
                    case CompareOperation.LessThanOrEqual:
                        if (known[instruction.Expression.First] <= instruction.Expression.Second)
                        {
                            known[instruction.Id] = instruction.Direction == Direction.Decrease ? known[instruction.Id] - instruction.AmountBy : known[instruction.Id] + instruction.AmountBy;
                        }
                        break;
                    case CompareOperation.GreaterThan:
                        if (known[instruction.Expression.First] > instruction.Expression.Second)
                        {
                            known[instruction.Id] = instruction.Direction == Direction.Decrease ? known[instruction.Id] - instruction.AmountBy : known[instruction.Id] + instruction.AmountBy;
                        }
                        break;
                    case CompareOperation.GreatherThanOrEqual:
                        if (known[instruction.Expression.First] >= instruction.Expression.Second)
                        {
                            known[instruction.Id] = instruction.Direction == Direction.Decrease ? known[instruction.Id] - instruction.AmountBy : known[instruction.Id] + instruction.AmountBy;
                        }
                        break;
                    case CompareOperation.NotEqual:
                        if (known[instruction.Expression.First] != instruction.Expression.Second)
                        {
                            known[instruction.Id] = instruction.Direction == Direction.Decrease ? known[instruction.Id] - instruction.AmountBy : known[instruction.Id] + instruction.AmountBy;
                        }
                        break;
                    case CompareOperation.Equal:
                        if (known[instruction.Expression.First] == instruction.Expression.Second)
                        {
                            known[instruction.Id] = instruction.Direction == Direction.Decrease ? known[instruction.Id] - instruction.AmountBy : known[instruction.Id] + instruction.AmountBy;
                        }
                        break;
                    default:
                        break;
                }
            }

            return known.OrderByDescending(k => k.Value).FirstOrDefault().Value;
        }

        private static List<Instruction> GetInstructions()
        {
            var instructions = new List<Instruction>();

            var file = new StreamReader(FilePath);
            string line;

            while ((line = file.ReadLine()) != null)
            {
                var instructionExpressions = line.Split(" ".ToCharArray());
                var instruction = new Instruction();
                instruction.Id = instructionExpressions[0];
                instruction.Direction = instructionExpressions[1] == "inc" ? Direction.Increase : Direction.Decrease;
                instruction.AmountBy = int.Parse(instructionExpressions[2]);
                instruction.Expression = new Expression();
                instruction.Expression.First = instructionExpressions[4];
                instruction.Expression.CompareOperation = GetCompareOperation(instructionExpressions[5]);
                instruction.Expression.Second = int.Parse(instructionExpressions[6]);

                instructions.Add(instruction);
            }

            return instructions;
        }

        private static CompareOperation GetCompareOperation(string compareOperationString)
        {
            switch (compareOperationString)
            {
                case "<":
                    return CompareOperation.LessThan;
                case "<=":
                    return CompareOperation.LessThanOrEqual;
                case ">":
                    return CompareOperation.GreaterThan;
                case ">=":
                    return CompareOperation.GreatherThanOrEqual;
                case "!=":
                    return CompareOperation.NotEqual;
                case "==":
                    return CompareOperation.Equal;
                default:
                    throw new Exception("Unrecognized Operation");
            }
        }

        private class Instruction
        {
            public string Id { get; set; }
            public Direction Direction { get; set; }
            public int AmountBy { get; set; }
            public Expression Expression { get; set; }
        }

        private enum Direction
        {
            Increase,
            Decrease
        }

        private enum CompareOperation
        {
            LessThan,
            LessThanOrEqual,
            GreaterThan,
            GreatherThanOrEqual,
            NotEqual,
            Equal
        }

        private class Expression
        {
            public string First { get; set; }
            public CompareOperation CompareOperation { get; set; }
            public int Second { get; set; }
        }
    }
}
