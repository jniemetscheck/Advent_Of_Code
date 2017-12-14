using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public static class Day12
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day12.txt";

        public static int GetPart1Result()
        {
            return GetResult(GetPrograms());
        }

        public static int GetResult(Dictionary<int, List<int>> programs)
        {
            var programsInGroup = new HashSet<int>();
            programsInGroup.Add(0);

            programsInGroup = GetUniqueHashSet(0, programs.FirstOrDefault(x => x.Key == 0).Value, programs, programsInGroup);

            return programsInGroup.Count;
        }

        public static HashSet<int> GetUniqueHashSet(int parentProgram, List<int> associatedPrograms, Dictionary<int, List<int>> programs, HashSet<int> existingHashset)
        {
            if (associatedPrograms != null && associatedPrograms.Any())
            {
                foreach (var program in associatedPrograms)
                {
                    if (!existingHashset.Contains(program))
                    {
                        existingHashset.Add(program);
                    }
                    var childPrograms = programs.FirstOrDefault(x => x.Key == program).Value;
                    if (childPrograms != null && childPrograms.Any())
                    {
                        existingHashset = GetUniqueHashSet(program, childPrograms.Where(y => !existingHashset.Contains(y)).ToList(), programs, existingHashset);
                    }
                }
            }

            return existingHashset;
        }

        private static Dictionary<int, List<int>> GetPrograms()
        {
            var programs = new Dictionary<int, List<int>>();

            var file = new StreamReader(FilePath);
            string line;

            while ((line = file.ReadLine()) != null)
            {
                var first = int.Parse(line.Split('<')[0].Trim());
                var secondSplit = line.Split('>')[1].Replace(" ", "");
                var children = new List<int>();
                foreach (var value in secondSplit.Split(','))
                {
                    children.Add(int.Parse(value));
                }
                programs.Add(first, children);
            }

            return programs;
        }
    }
}
