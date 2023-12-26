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
            var hands = GetMappedHands(lines.ToList(), 11);

            return GetTotalWinnings(hands);
        }

        public static double GetResultPartTwo()
        {
            var lines = File.ReadAllLines(FilePath);
            var hands = GetMappedHands(lines.ToList(), 1);

            return GetTotalWinnings(hands);
        }

        public static List<Hand> GetMappedHands(List<string> lines, int jValue)
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
                            newCard.Value = jValue;
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

                if (jValue == 1)
                {
                    //get best hand using jokers
                    hand.Type = GetHandTypeWithJokers(hand, jValue);
                }
                else
                {
                    hand.Type = GetHandType(hand.Cards);
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

        public static HandType GetHandTypeWithJokers(Hand hand, int jValue)
        {
            var dictionary = new Dictionary<int, int>();

            foreach (var card in hand.Cards)
            {
                if (card.Value != 1)
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
            }

            var jCount = hand.Cards.Count(c => c.Value == jValue);

            var highest = dictionary.OrderByDescending(o => o.Value).ThenByDescending(o => o.Key).FirstOrDefault();

            if (highest.Key == 0)
            {
                //all Js
                dictionary.Add(1, jCount);
            }
            else
            {
                dictionary[highest.Key] += jCount;
            }

            return GetHandTypeForDictionary(dictionary);
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

            return GetHandTypeForDictionary(dictionary);
        }

        public static HandType GetHandTypeForDictionary(Dictionary<int, int> dictionary)
        {
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