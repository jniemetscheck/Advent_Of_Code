using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2023.Days
{
    public static class Day04
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day04.txt";

        public static double GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);
            var cards = GetMappedCards(lines.ToList());

            return GetWinnings(cards);
        }

        public static double GetResultPartTwo()
        {
            var lines = File.ReadAllLines(FilePath);
            var cards = GetMappedCards(lines.ToList());

            return GetWinningsModified(cards);
        }

        public static List<Card> GetMappedCards(List<string> input)
        {
            var cards = new List<Card>();
            var normalizedInput = GetNormalizedLines(input);

            foreach (var line in normalizedInput)
            {
                var card = new Card { WinningNumbers = new List<int>(), DrawnNumbers = new List<int>(), Copies = new List<Card>() };

                var cardSplit = line.Split(':');
                var numbersSplit = cardSplit[1].Split('|');

                foreach (var winningNumber in numbersSplit[0].Trim().Split(' '))
                {
                    card.WinningNumbers.Add(int.Parse(winningNumber));
                }

                foreach (var drawnNumber in numbersSplit[1].Trim().Split(' '))
                {
                    card.DrawnNumbers.Add(int.Parse(drawnNumber));
                }

                cards.Add(card);
            }

            return cards;
        }

        public static List<string> GetNormalizedLines(List<string> input)
        {
            var result = new List<string>();

            foreach (var line in input)
            {
                var newLine = line;

                for (var i = 1; i < 10; i++)
                {
                    newLine = newLine.Replace(" " + i + " ", "0" + i + " ");

                    if (newLine[newLine.Length - 2] == ' ')
                    {
                        newLine = newLine.Remove(newLine.Length - 2, 1).Insert(newLine.Length - 2, "0");
                    }
                }

                result.Add(newLine);
            }

            return result;
        }

        public static double GetWinnings(List<Card> cards)
        {
            var result = 0d;

            foreach (var card in cards)
            {
                var winningNumberCount = 0;

                foreach (var winningNumber in card.WinningNumbers)
                {
                    foreach (var drawnNumber in card.DrawnNumbers)
                    {
                        if (winningNumber == drawnNumber)
                        {
                            winningNumberCount++;
                        }
                    }
                }

                if (winningNumberCount > 0)
                {
                    result += Math.Pow(2, winningNumberCount - 1);
                }
            }

            return result;
        }

        public static double GetWinningsModified(List<Card> cards)
        {
            var result = 0d;

            for (var i = 0; i < cards.Count; i++)
            {
                var count = GetWinningCount(i, cards);

                for (var j = i + 1; j <= i + count; j++)
                {
                    if (j < cards.Count)
                    {
                        cards[i].Copies.Add(cards[j]);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            foreach (var card in cards)
            {
                result += GetWinCount(card.Copies);
            }

            return result;
        }

        public static int GetWinCount(List<Card> copies)
        {
            var result = 0;

            foreach (var copy in copies)
            {
                result += GetWinCount(copy.Copies);
            }

            result++;

            return result;
        }

        public static int GetWinningCount(int index, List<Card> cards)
        {
            var winningNumberCount = 0;

            foreach (var winningNumber in cards[index].WinningNumbers)
            {
                foreach (var drawnNumber in cards[index].DrawnNumbers)
                {
                    if (winningNumber == drawnNumber)
                    {
                        winningNumberCount++;
                    }
                }
            }

            return winningNumberCount;
        }
    }

    public class Card
    {
        public List<int> WinningNumbers { get; set; }
        public List<int> DrawnNumbers { get; set; }
        public List<Card> Copies { get; set; }
    }
}