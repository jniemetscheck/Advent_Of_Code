using System.Collections.Generic;

namespace AdventOfCode2023.Days
{
    public class Day06
    {

        public static double GetResultPartOne()
        {
            return GetPossibleWaysToBeatRecord(GetRaces(false), 1);
        }

        public static double GetResultPartTwo()
        {
            return GetPossibleWaysToBeatRecord(GetRaces(true), 1);
        }

        public static List<Race> GetRaces(bool modified)
        {
            if (!modified)
            {
                //return new List<Race>
                //{
                //    GetRace(7, 9),
                //    GetRace(15, 40),
                //    GetRace(30, 200)
                //};
                return new List<Race>
                {
                    GetRace(44, 277),
                    GetRace(89, 1136),
                    GetRace(96, 1890),
                    GetRace(91, 1768)
                };
            }
            else
            {
                //return new List<Race>
                //{
                //    GetRace(71530, 940200)
                //};
                return new List<Race>
                {
                    GetRace(44899691, 277113618901768)
                };
            }
        }

        public static Race GetRace(double time, double recordDistance)
        {
            return new Race { Time = time, RecordDistance = recordDistance };
        }

        public static double GetPossibleWaysToBeatRecord(List<Race> races, double speedIncrementer)
        {
            var result = 1d;
            var possibilities = new List<double>();

            foreach (var t in races)
            {
                possibilities.Add(GetPossibleWaysToBeatRecordCount(t, speedIncrementer));
            }

            foreach (var possibility in possibilities)
            {
                result *= possibility;
            }

            return result;
        }

        public static int GetPossibleWaysToBeatRecordCount(Race race, double speedIncrementer)
        {
            var possibilityCount = 0;
            var currentSpeed = speedIncrementer;

            for (var i = 1; i <= race.Time; i++)
            {
                if (GetRaceDistance(i, race.Time, currentSpeed) > race.RecordDistance)
                {
                    possibilityCount++;
                }

                currentSpeed += speedIncrementer;
            }

            return possibilityCount;
        }

        public static double GetRaceDistance(double holdButtonTime, double totalRaceTime, double speed)
        {
            var movingTime = totalRaceTime - holdButtonTime;

            return movingTime * speed;
        }
    }

    public class Race
    {
        public double Time { get; set; }
        public double RecordDistance { get; set; }
    }
}