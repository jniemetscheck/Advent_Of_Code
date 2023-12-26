using AdventOfCode2023.Classes.Day07;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2023.Days
{
    public class Day07
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day07.txt";

        public static double GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);
            var hands = GetMappedHands(lines.ToList(), false);

            return GetTotalWinnings(hands);
        }

        //public static double GetResultPartTwo()
        //{
        //var lines = File.ReadAllLines(FilePath);
        //var hands = GetMappedHands(lines.ToList());

        //    //return GetClosestSeedLocation(garden);
        //    return 0;
        //}

        public static List<Hand> GetMappedHands(List<string> lines, bool sortHand)
        {
            var result = new List<Hand>();

            foreach (var line in lines)
            {
                var hand = new Hand { Cards = new List<Card>() };
                var handBidSplit = line.Split(' ');

                foreach (var card in handBidSplit[0])
                {
                    var newCard = new Card { Label = card.ToString().ToLower() };

                    switch (card.ToString().ToLower())
                    {
                        case "a":
                            newCard.Value = 14;
                            break;
                        case "k":
                            newCard.Value = 13;
                            break;
                        case "q":
                            newCard.Value = 12;
                            break;
                        case "j":
                            newCard.Value = 11;
                            break;
                        case "t":
                            newCard.Value = 10;
                            break;
                        default:
                            newCard.Value = int.Parse(card.ToString());
                            break;
                    }

                    hand.Cards.Add(newCard);
                }

                hand.Bid = int.Parse(handBidSplit[1]);
                hand.Type = GetHandType(hand.Cards);

                if (sortHand)
                {
                    hand.Cards = hand.Cards.OrderByDescending(o => o.Value).ToList();
                }

                result.Add(hand);
            }

            return result;
        }

        public static double GetTotalWinnings(List<Hand> hands)
        {
            var result = 0d;
            var rankedHands = new List<Hand>();

            rankedHands.AddRange(GetRankedHands(hands.Where(t => t.Type == HandType.HighCard)));
            rankedHands.AddRange(GetRankedHands(hands.Where(t => t.Type == HandType.OnePair)));
            rankedHands.AddRange(GetRankedHands(hands.Where(t => t.Type == HandType.TwoPair)));
            rankedHands.AddRange(GetRankedHands(hands.Where(t => t.Type == HandType.ThreeOfAKind)));
            rankedHands.AddRange(GetRankedHands(hands.Where(t => t.Type == HandType.FullHouse)));
            rankedHands.AddRange(GetRankedHands(hands.Where(t => t.Type == HandType.FourOfAKind)));
            rankedHands.AddRange(GetRankedHands(hands.Where(t => t.Type == HandType.FiveOfAKind)));

            var currentMultiplier = 1;
            foreach (var rankedHand in rankedHands)
            {
                result += currentMultiplier * rankedHand.Bid;
                currentMultiplier++;
            }

            return result;
        }

        public static HandType GetHandType(List<Card> cards)
        {
            var dictionary = new Dictionary<int, int>();

            foreach (var card in cards)
            {
                if (dictionary.ContainsKey(card.Value))
                {
                    dictionary[card.Value]++;
                }
                else
                {
                    dictionary.Add(card.Value, 1);
                }
            }

            if (dictionary.ContainsValue(5))
            {
                return HandType.FiveOfAKind;
            }

            if (dictionary.ContainsValue(4))
            {
                return HandType.FourOfAKind;
            }

            if (dictionary.ContainsValue(3) && dictionary.ContainsValue(2))
            {
                return HandType.FullHouse;
            }

            if (dictionary.ContainsValue(3))
            {
                return HandType.ThreeOfAKind;
            }

            if (dictionary.ContainsValue(2) && dictionary.Count(c => c.Value == 2) == 2)
            {
                return HandType.TwoPair;
            }

            if (dictionary.ContainsValue(2) && dictionary.Count(c => c.Value == 2) == 1)
            {
                return HandType.OnePair;
            }

            return HandType.HighCard;
        }

        public static List<Hand> GetRankedHands(IEnumerable<Hand> hands)
        {
            var result = new List<Hand>();

            foreach (var hand in hands.OrderBy(o => o.Cards[0].Value).ThenBy(o => o.Cards[1].Value).ThenBy(o => o.Cards[2].Value).ThenBy(o => o.Cards[3].Value).ThenBy(o => o.Cards[4].Value))
            {
                result.Add(hand);
            }

            return result;
        }
    }
}