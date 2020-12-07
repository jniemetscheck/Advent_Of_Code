using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Days
{
    public static class Day07
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day07.txt";

        public static int GetResultPartOne()
        {
            var input = File.ReadAllLines(FilePath);

            var rules = GetMappedBagRules(input.ToList());

            return BagsThatContainAnotherBag("shiny gold", rules).Distinct().Count();
        }

        public static int GetResultPartTwo()
        {
            var input = File.ReadAllLines(FilePath);

            var rules = GetMappedBagRules(input.ToList());

            return GetBagsNeeded("shiny gold", rules);
        }

        public static List<string> BagsThatContainAnotherBag(string anotherBag, List<BagRule> rules)
        {
            var bagColors = new List<string>();

            var containingBags = rules.Where(r => r.ContainedBags.ContainsKey(anotherBag)).ToList();

            foreach (var containingBag in containingBags)
            {
                bagColors.Add(containingBag.Description);
                bagColors.AddRange(BagsThatContainAnotherBag(containingBag.Description, rules));
            }

            return bagColors;
        }

        public static int GetBagsNeeded(string bag, List<BagRule> rules)
        {
            var count = 0;
            var outerBag = rules.FirstOrDefault(r => r.Description == bag);

            if (outerBag != null)
            {
                foreach (var containedBag in outerBag.ContainedBags)
                {
                    count += containedBag.Value;
                    count += containedBag.Value * GetBagsNeeded(containedBag.Key, rules);
                }
            }

            return count;
        }

        public static List<BagRule> GetMappedBagRules(List<string> bagRules)
        {
            var result = new List<BagRule>();

            foreach (var bagRule in bagRules)
            {
                var rule = new BagRule();

                var bagRuleMainSplit = Regex.Split(bagRule, "contain");

                rule.Description = bagRuleMainSplit[0].Trim().Split(" ".ToCharArray())[0] + " " + bagRuleMainSplit[0].Trim().Split(" ".ToCharArray())[1];
                rule.ContainedBags = new Dictionary<string, int>();

                var containedBagsSplit = bagRuleMainSplit[1].Trim().Split(",".ToCharArray());

                foreach (var containedBagSplit in containedBagsSplit)
                {
                    var spaceSplit = containedBagSplit.Trim().Split(" ".ToCharArray());

                    if (spaceSplit[0] == "no")
                    {
                        break;
                    }

                    //var containedBag = new ContainedBag
                    //{
                    //    Quantity = int.Parse(spaceSplit[0]), 
                    //    Description = spaceSplit[1] + " " + spaceSplit[2]
                    //};

                    rule.ContainedBags.Add(spaceSplit[1] + " " + spaceSplit[2], int.Parse(spaceSplit[0]));
                }

                result.Add(rule);
            }

            return result;
        }
    }

    public class BagRule
    {
        public string Description { get; set; }
        public Dictionary<string, int> ContainedBags { get; set; }
    }

    public class ContainedBag
    {
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
