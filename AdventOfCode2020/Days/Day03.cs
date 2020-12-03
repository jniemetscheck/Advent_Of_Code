using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day03
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day03.txt";

        public static double GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetTreesEncountered(lines.ToList(), 3, 1);
        }

        public static double GetResultPartTwo()
        {
            var lines = File.ReadAllLines(FilePath);

            var one = GetTreesEncountered(lines.ToList(), 1, 1);
            var two = GetTreesEncountered(lines.ToList(), 3, 1);
            var three = GetTreesEncountered(lines.ToList(), 5, 1);
            var four = GetTreesEncountered(lines.ToList(), 7, 1);
            var five = GetTreesEncountered(lines.ToList(), 1, 2);

            return one * two * three * four * five;
        }

        public static double GetTreesEncountered(List<string> rows, int right, int down)
        {
            var treesEncountered = 0;
            var currentRow = down;
            var currentColumn = right;

            while (currentRow < rows.Count)
            {
                if (currentColumn + right > rows[currentRow].Length)
                {
                    AddLengthToRows(rows);
                }
                if (rows[currentRow][currentColumn] == '#')
                {
                    treesEncountered++;
                }

                currentRow += down;
                currentColumn += right;
            }

            return treesEncountered;
        }

        public static void AddLengthToRows(List<string> rows)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                rows[i] = rows[i] + rows[i];
            }
        }
    }
}
