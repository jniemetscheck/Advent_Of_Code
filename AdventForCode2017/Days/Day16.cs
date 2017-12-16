using AdventOfCode2017.Days.Sixteen;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public static class Day16
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day16.txt";

        public static string GetPart1Result()
        {
            var moves = GetDanceMoves();
            return GetPart1Result(moves.Sequences, moves.DanceMoves);
        }

        public static string GetPart2Result()
        {
            var moves = GetDanceMoves();
            var partOneResult = GetPart1Result(moves.Sequences, moves.DanceMoves);
            var results = new List<string>();
            results.Add(partOneResult);

            for (int i = 1; i < 1000000000; i++)
            {
                var nextResult = GetPart1Result(partOneResult.Select(c => c.ToString()).ToList(), moves.DanceMoves);
                if (results.Contains(nextResult))
                {
                    //hey, we have a circle here
                    var cyclesAfter = i;
                    var position = 1000000000 % i;
                    return results[position - 1];
                }
                else
                {
                    partOneResult = nextResult;
                    results.Add(nextResult);
                }
            }

            return partOneResult;
        }

        public static string GetPart1Result(List<string> sequences, List<DanceMove> danceMoves)
        {
            foreach (var move in danceMoves)
            {
                switch (move.Move)
                {
                    case Move.Spin:
                        sequences = GetSpinResult(sequences, (int)move.SpinAmount);
                        break;
                    case Move.Exchange:
                        sequences = GetExchangeResult(sequences, (int)move.ExchangeFirst, (int)move.ExchangeSecond);
                        break;
                    case Move.Partner:
                        sequences = GetPartnerResult(sequences, move.PartnerFirst, move.PartnerSecond);
                        break;
                    default:
                        break;
                }
            }

            return string.Join("", sequences.ToArray());
        }

        private static List<string> GetSpinResult(List<string> sequences, int moveNumber)
        {
            var temp = new List<string>(sequences);

            for (int i = 0; i < sequences.Count; i++)
            {
                if (i + moveNumber < temp.Count)
                {
                    temp[i + moveNumber] = sequences[i];
                }
                else
                {
                    temp[i + moveNumber - sequences.Count] = sequences[i];
                }
            }

            return temp;
        }

        private static List<string> GetExchangeResult(List<string> sequences, int firstIndex, int secondIndex)
        {
            var firstValue = sequences[firstIndex];
            var secondValue = sequences[secondIndex];
            sequences[firstIndex] = secondValue;
            sequences[secondIndex] = firstValue;
            return sequences;
        }

        private static List<string> GetPartnerResult(List<string> sequences, string firstValue, string secondValue)
        {
            var firstIndex = sequences.IndexOf(firstValue);
            var secondIndex = sequences.IndexOf(secondValue);
            sequences[secondIndex] = firstValue;
            sequences[firstIndex] = secondValue;

            return sequences;
        }

        private static (List<string> Sequences, List<DanceMove> DanceMoves) GetDanceMoves()
        {
            var sequences = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p" };
            var danceMoves = new List<DanceMove>();
            var allInstructions = File.ReadAllText(FilePath).Split(',');

            foreach (var instruction in allInstructions)
            {
                var danceMove = new DanceMove();
                var move = instruction.Substring(0, 1);
                var rest = instruction.Substring(1, instruction.Length - 1).Split('/');

                if (move.ToUpper() == "S")
                {
                    danceMove.Move = Move.Spin;
                    danceMove.SpinAmount = int.Parse(rest[0]);
                }
                else if(move.ToUpper() == "X")
                {
                    danceMove.Move = Move.Exchange;
                    danceMove.ExchangeFirst = int.Parse(rest[0]);
                    danceMove.ExchangeSecond = int.Parse(rest[1]);
                }
                else
                {
                    danceMove.Move = Move.Partner;
                    danceMove.PartnerFirst = rest[0];
                    danceMove.PartnerSecond = rest[1];
                }

                danceMoves.Add(danceMove);
            }

            return (sequences, danceMoves);
        }
    }
}

namespace AdventOfCode2017.Days.Sixteen
{
    public class DanceMove
    {
        public Move Move { get; set; }
        public int? SpinAmount { get; set; }
        public int? ExchangeFirst { get; set; }
        public int? ExchangeSecond { get; set; }
        public string PartnerFirst { get; set; }
        public string PartnerSecond { get; set; }
    }

    public enum Move
    {
        Spin,
        Exchange,
        Partner
    }
}
