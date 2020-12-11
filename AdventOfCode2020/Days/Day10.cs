//Part B attributed to 
//https://github.com/DanaL/AdventOfCode/blob/master/2020/Day10.cs

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day10
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day10.txt";

        public static int GetResultPartOne()
        {
            var input = File.ReadAllLines(FilePath).Select(int.Parse).ToList();

            return GetJoltCount(input);
        }

        public static int GetJoltCount(List<int> adapters)
        {
            var oneJoltDifferences = 0;
            var threeJoltDifferences = 0;
            var currentJoltage = 0;

            foreach (var adapter in adapters.OrderBy(o => o))
            {
                if (adapter - currentJoltage == 1)
                {
                    oneJoltDifferences++;
                }

                if (adapter - currentJoltage == 3)
                {
                    threeJoltDifferences++;
                }

                currentJoltage = adapter;
            }

            //account for highest adapter
            threeJoltDifferences++;

            return oneJoltDifferences * threeJoltDifferences;
        }

        public static bool IsValid(List<int> adapters)
        {
            var isValid = true;
            var currentJoltage = 0;

            foreach (var adapter in adapters.OrderBy(o => o))
            {
                if (adapter - currentJoltage > 3)
                {
                    isValid = false;
                    break;
                }

                currentJoltage = adapter;
            }

            return isValid;
        }
    }

    public class Day10PartB
    {
        private readonly Dictionary<int, long> _branches;

        public Day10PartB()
        {
            _branches = new Dictionary<int, long>();
        }

        public long Solve()
        {
            TextReader tr = new StreamReader("input/day10.txt");
            var adapters = tr.ReadToEnd().Split('\n')
                .Select(int.Parse).OrderBy(n => n).ToArray();

            var v = adapters.Zip(adapters.Skip(1), (a, b) => b - a);

            _branches[adapters.Max()] = 0;
            long p2 = 0;
            int i = 0;
            do
            {
                p2 += 1 + branchesFrom(adapters, i);
                ++i;
            } while (adapters[i] <= 3);

            return p2;
        }

        private long branchesFrom(int[] adapters, int index)
        {
            int a = adapters[index];

            if (_branches.ContainsKey(a))
                return _branches[a];

            long count = 0;
            int x = index + 1;
            while (x < adapters.Length && adapters[x] - a <= 3)
            {
                int b = adapters[x];
                count += 1 + (_branches.ContainsKey(b) ? _branches[b] : branchesFrom(adapters, x));
                ++x;
            }

            _branches[a] = count - 1;

            return _branches[a];
        }
    }
}
