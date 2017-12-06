using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventForCode2017.Days
{
    public static class Day04
    {
        public static int GetPart1Result()
        {
            var validNumber = 0;
            var passPhrases = GetAllPassphrases();

            foreach (var passPhrase in passPhrases)
            {
                var hashSet = new HashSet<string>();
                var valid = true;
                foreach (var passPhrasePart in passPhrase)
                {
                    if (hashSet.Contains(passPhrasePart))
                    {
                        valid = false;
                        break;
                    }

                    hashSet.Add(passPhrasePart);
                }

                if (valid)
                {
                    validNumber++;
                }
            }

            return validNumber;
        }

        public static int GetPart2Result()
        {
            var validNumber = 0;
            var passPhrases = GetAllPassphrases();

            foreach (var passPhrase in passPhrases)
            {
                var hashSet = new HashSet<string>();
                var valid = true;
                foreach (var passPhrasePart in passPhrase)
                {
                    var sortedPassphrase = string.Join("", passPhrasePart.OrderBy(o => o).ToArray());
                    if (hashSet.Contains(sortedPassphrase))
                    {
                        valid = false;
                        break;
                    }

                    hashSet.Add(sortedPassphrase);
                }

                if (valid)
                {
                    validNumber++;
                }
            }

            return validNumber;
        }

        private static List<List<string>> GetAllPassphrases()
        {
            var result = new List<List<string>>();
            StreamReader file = new System.IO.StreamReader(@"C:\GitHub\AdventOfCode2017\AdventForCode2017\bin\Debug\Input\Day04.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {
                result.Add(new List<string>(line.Split(' ')));
            }

            return result;
        }
    }
}
