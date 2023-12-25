using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2023.Days
{
    public class Day06
    {

        public static double GetResultPartOne()
        {
            return GetPossibleWaysToBeatRecord(GetRaces(), 1);
        }

        //public static double GetResultPartTwo()
        //{
        //    return GetPossibleWaysToBeatRecord(GetRaces());
        //}

        public static List<Race> GetRaces()
        {
            //var races = new List<Race>
            //{
            //    GetRace(7, 9),
            //    GetRace(15, 40),
            //    GetRace(30, 200)
            //};
            var races = new List<Race>
            {
                GetRace(44, 277),
                GetRace(89, 1136),
                GetRace(96, 1890),
                GetRace(91, 1768)
            };

            return races;
        }

        public static Race GetRace(int time, int recordDistance)
        {
            return new Race { Time = time, RecordDistance = recordDistance };
        }

        public static double GetPossibleWaysToBeatRecord(List<Race> races, int speedIncrementer)
        {
            var result = 1d;
            var possibilities = new List<int>();

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

        public static int GetPossibleWaysToBeatRecordCount(Race race, int speedIncrementer)
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

        public static int GetRaceDistance(int holdButtonTime, int totalRaceTime, int speed)
        {
            var movingTime = totalRaceTime - holdButtonTime;

            return movingTime * speed;
        }
    }

    public class Race
    {
        public int Time { get; set; }
        public int RecordDistance { get; set; }
    }
}