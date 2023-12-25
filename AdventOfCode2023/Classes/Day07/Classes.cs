using System.Collections.Generic;

namespace AdventOfCode2023.Classes.Day07
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public int Bid { get; set; }
    }

    public class Card
    {
        public string Label { get; set; }
        public int Value { get; set; }
    }
}