using System.IO;

namespace AdventForCode2017.Days
{
    public static class Day01
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day01.txt";

        public static int GetPart1Result()
        {
            var input = File.ReadAllText(FilePath);
            var sum = 0;

            if (input.Length > 0)
            {
                var currentDigit = "";
                foreach (var digit in input)
                {
                    sum += GetSum(currentDigit, digit.ToString());
                    currentDigit = digit.ToString();
                }

                sum += GetSum(currentDigit, input[0].ToString());
            }

            return sum;
        }

        public static int GetPart2Result()
        {
            var input = File.ReadAllText(FilePath);
            var sum = 0;

            if (input.Length > 0)
            {
                var stepsAhead = input.Length / 2;

                for (int i = 0; i < input.Length; i++)
                {
                    var index = i + stepsAhead;
                    if (index >= input.Length)
                    {
                        //we're at end, go back to 0
                        index = index - input.Length;
                    }
                    sum += GetSum(input[i].ToString(), input[index].ToString());
                }
            }

            return sum;
        }

        private static int GetSum(string currentDigit, string digit)
        {
            if (currentDigit == digit.ToString())
            {
                return int.Parse(digit.ToString());
            }

            return 0;
        }
    }
}
