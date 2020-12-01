using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Days
{
    public static class Day01
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day01.txt";

        public static int GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetFuelRequiredToLaunch(lines.ToList());
        }

        public static int GetFuelRequiredToLaunch(List<string> moduleMasses)
        {
            var mass = 0;

            foreach (var moduleMass in moduleMasses)
            {
                var currentMass = int.Parse(moduleMass);

                mass += (int)Math.Floor((decimal)currentMass / 3) - 2;
            }

            return mass;
        }
    }
}
