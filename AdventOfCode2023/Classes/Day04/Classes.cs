using System.Collections.Generic;

namespace AdventOfCode2023.Classes.Day04
{
    public class Card
    {
        public List<int> WinningNumbers { get; set; }
        public List<int> DrawnNumbers { get; set; }
        public List<Card> Copies { get; set; }
    }
}