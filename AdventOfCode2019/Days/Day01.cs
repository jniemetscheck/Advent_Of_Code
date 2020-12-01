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

        public static int GetResultPartTwo()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetTotalFuelRequirements(lines.ToList());
        }

        public static int GetFuelRequiredToLaunch(List<string> moduleMasses)
        {
            var mass = 0;

            foreach (var moduleMass in moduleMasses)
            {
                mass += GetFuelForMass(int.Parse(moduleMass));
            }

            return mass;
        }

        private static int GetFuelForMass(int mass)
        {
            return (int)Math.Floor((decimal)mass / 3) - 2;
        }

        public static int GetTotalFuelRequirements(List<string> moduleMasses)
        {
            var totalMass = 0;

            foreach (var moduleMass in moduleMasses)
            {
                totalMass += GetFuelRequiredForFuel(GetFuelForMass(int.Parse(moduleMass)));
            }

            return totalMass;
        }

        public static int GetFuelRequiredForFuel(int moduleMass)
        {
            var fuelRequired = (int)Math.Floor((decimal)moduleMass / 3) - 2;

            if (fuelRequired <= 0)
            {
                fuelRequired = 0;
            }
            else
            {
                fuelRequired = GetFuelRequiredForFuel(fuelRequired);
            }

            return moduleMass + fuelRequired;
        }
    }
}
