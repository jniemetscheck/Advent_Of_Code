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
            var hands = GetMappedHands(lines.ToList());

            //return GetClosestSeedLocation(garden);
            return 0;
        }

        //public static double GetResultPartTwo()
        //{
        //var lines = File.ReadAllLines(FilePath);
        //var hands = GetMappedHands(lines.ToList());

        //    //return GetClosestSeedLocation(garden);
        //    return 0;
        //}

        public static List<Hand> GetMappedHands(List<string> lines)
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

                hand.Cards = hand.Cards.OrderByDescending(o => o.Value).ToList();

                result.Add(hand);
            }

            return result;
        }
    }
}