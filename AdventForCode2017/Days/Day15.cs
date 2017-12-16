using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days
{
    public static class Day15
    {
        private const int aMultiplier = 16807;
        private const int bMultiplier = 48271;
        private const int dividend = 2147483647;

        public static int GetPart1Result()
        {
            return GetPart1Result(722, 354);
        }

        public static int GetPart2Result()
        {
            return GetPart2Result(722, 354);
        }

        public static int GetPart1Result(long aValue, long bValue)
        {
            var matches = 0;

            for (int i = 1; i <= 40000000; i++)
            {
                aValue = aValue * aMultiplier % dividend;
                bValue = bValue * bMultiplier % dividend;

                matches = DoesMatch(aValue, bValue) ? matches + 1 : matches;
            }

            return matches;
        }

        public static int GetPart2Result(long aValue, long bValue)
        {
            var matches = 0;
            var validAValues = new List<long>();
            var validBValues = new List<long>();

            for (int i = 1; i <= 40000000; i++)
            {
                aValue = aValue * aMultiplier % dividend;
                bValue = bValue * bMultiplier % dividend;

                if (aValue % 4 == 0)
                {
                    validAValues.Add(aValue);
                }
                if (bValue % 8 == 0)
                {
                    validBValues.Add(bValue);
                }
            }

            int goUntil = 5000000;
            if (validAValues.Count < goUntil || validBValues.Count < goUntil)
            {
                goUntil = validAValues.Count < validBValues.Count ? validAValues.Count : validBValues.Count;
            }
            for (int i = 0; i < goUntil; i++)
            {
                matches = DoesMatch(validAValues[i], validBValues[i]) ? matches + 1 : matches;
            }

            return matches;
        }

        private static bool DoesMatch(long aValue, long bValue)
        {
            var aBinary = Convert.ToString(aValue, 2).PadLeft(32, '0');
            var bBinary = Convert.ToString(bValue, 2).PadLeft(32, '0');

            return aBinary.Substring(aBinary.Length - 16, 16) == bBinary.Substring(bBinary.Length - 16, 16);
        }
    }
}
