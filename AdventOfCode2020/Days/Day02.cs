using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day02
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day02.txt";

        public static int GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);
            var passwordRules = GetPasswordRules(lines.ToList());

            return GetValidPasswordCountAssumedRules(passwordRules);
        }

        public static int GetResultPartTwo()
        {
            var lines = File.ReadAllLines(FilePath);
            var passwordRules = GetPasswordRules(lines.ToList());

            return GetValidPasswordCountActualRules(passwordRules);
        }

        public static List<PasswordRule> GetPasswordRules(List<string> input)
        {
            var result = new List<PasswordRule>();

            foreach (var rule in input)
            {
                var ruleSplit = rule.Split(": ".ToCharArray());
                var minimum = ruleSplit[0].Split("-".ToCharArray())[0];
                var maximum = ruleSplit[0].Split("-".ToCharArray())[1];
                var letter = ruleSplit[1];
                var password = ruleSplit[3];

                var newRule = new PasswordRule();
                newRule.Minimum = int.Parse(minimum);
                newRule.Maximum = int.Parse(maximum);
                newRule.Letter = letter.ToCharArray()[0];
                newRule.Password = password;

                result.Add(newRule);
            }

            return result;
        }

        public static int GetValidPasswordCountAssumedRules(List<PasswordRule> passwordRules)
        {
            var count = 0;

            foreach (var passwordRule in passwordRules)
            {
                var characterCount = passwordRule.Password.Count(c => c == passwordRule.Letter);

                if (characterCount >= passwordRule.Minimum && characterCount <= passwordRule.Maximum)
                {
                    count++;
                }
            }

            return count;
        }

        public static int GetValidPasswordCountActualRules(List<PasswordRule> passwordRules)
        {
            var count = 0;

            foreach (var passwordRule in passwordRules)
            {
                var passwordRuleArray = passwordRule.Password.ToCharArray().Select(c => c.ToString()).ToArray();

                if ((passwordRuleArray[passwordRule.Minimum - 1].ToCharArray()[0] == passwordRule.Letter && passwordRuleArray[passwordRule.Maximum - 1].ToCharArray()[0] != passwordRule.Letter) || ((passwordRuleArray[passwordRule.Maximum - 1].ToCharArray()[0] == passwordRule.Letter && passwordRuleArray[passwordRule.Minimum - 1].ToCharArray()[0] != passwordRule.Letter)))
                {
                    count++;
                }
            }

            return count;
        }


    }

    public class PasswordRule
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public char Letter { get; set; }
        public string Password { get; set; }
    }
}
