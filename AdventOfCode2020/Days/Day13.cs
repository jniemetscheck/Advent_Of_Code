//credit rasqall

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day13
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day13.txt";

        public static long GetResultPartOne()
        {
            var input = File.ReadAllLines(FilePath).ToList();
            var buses = GetBuses(input);

            return GetEarliestBus(buses);
        }

        public static long GetResultPartTwo()
        {
            var input = File.ReadAllLines(FilePath).ToList();
            var buses = GetBuses(input);

            return GetEarliestTimestampSpecial(buses);
        }

        public static List<Bus> GetBuses(List<string> input)
        {
            var timestamp = long.Parse(input[0]);
            var buses = input[1]
                .Split(',')
                .Where(b => b != "x")
                .Select(s => new Bus()
                {
                    Id = long.Parse(s),
                    WaitTime = (long.Parse(s) - (timestamp % long.Parse(s))),
                    Index = input[1].Split(',').ToList().IndexOf(s)
                })
                .ToList();

            return buses;
        }

        public static long GetEarliestBus(List<Bus> buses)
        {
            var earliestBus = buses
                .OrderBy(b => b.WaitTime)
                .FirstOrDefault();

            return earliestBus.Id * earliestBus.WaitTime;
        }

        public static long GetEarliestTimestampSpecial(List<Bus> buses)
        {
            var set = buses.Select(b => new Equation()
            {
                A = b.Id - b.Index,
                N = b.Id
            }).ToList();

            var bigN = set.Aggregate((t, n) => new Equation() { A = t.A, N = t.N * n.N }).N;

            for (var i = 0; i < set.Count(); i++)
            {
                set[i].Np = bigN / set[i].N;
                for (var p = 1; set[i].U == 0; p++)
                    set[i].U = p * set[i].Np % set[i].N == 1 ? p : 0;
            }

            var ans = set.Select(e => e.A * e.Np * e.U).Sum() % bigN;

            return ans;
        }
    }

    public class Bus
    {
        public long Id { get; set; }
        public long WaitTime { get; set; }
        public int Index { get; set; }
    }

    public class Equation
    {
        public long A { get; set; }
        public long N { get; set; }
        public long Np { get; set; }
        public long U { get; set; }
    }
}
