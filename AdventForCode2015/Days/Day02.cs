using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2015.Days
{
    public class Day02
    {
        public static int GetPart1Result()
        {
            var total = 0;
            foreach (var gift in GetGifts())
            {
                var initialArea = (2 * gift.Dimensions[0] * gift.Dimensions[1]) + (2 * gift.Dimensions[1] * gift.Dimensions[2]) + (2 * gift.Dimensions[0] * gift.Dimensions[2]);
                var ordered = gift.Dimensions.OrderBy(o => o).ToList();
                var slack = ordered[0] * ordered[1];

                total += initialArea + slack;
            }

            return total;
        }

        public static int GetPart2Result()
        {
            var total = 0;
            foreach (var gift in GetGifts())
            {
                var initialArea = gift.Dimensions[0] * gift.Dimensions[1] * gift.Dimensions[2];
                var ordered = gift.Dimensions.OrderBy(o => o).ToList();
                var perimeter = (ordered[0] * 2) + (ordered[1] * 2);

                total += initialArea + perimeter;
            }

            return total;
        }

        private static List<Gift> GetGifts()
        {
            var result = new List<Gift>();
            var file = new System.IO.StreamReader(Directory.GetCurrentDirectory() + @"/Input/Day02.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {
                var input = line.Split('x');

                var gift = new Gift();
                gift.Dimensions = new List<int> { int.Parse(input[0]), int.Parse(input[1]), int.Parse(input[2]) };

                result.Add(gift);
            }

            return result;
        }
    }

    public class Gift
    {
        public List<int> Dimensions { get; set; }
    }
}
