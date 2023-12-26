using System.Collections.Generic;

namespace AdventOfCode2023.Classes.Day07
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public int Bid { get; set; }
        public HandType Type { get; set; }
    }

    public class Card
    {
        public string Label { get; set; }
        public int Value { get; set; }
    }

    public enum HandType
    {
        Unknown = 0,
        HighCard = 1,
        OnePair = 2,
        TwoPair = 3,
        ThreeOfAKind = 4,
        FullHouse = 5,
        FourOfAKind = 6,
        FiveOfAKind = 7
    }
}