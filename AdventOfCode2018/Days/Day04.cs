using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public static class Day04
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day04.txt";

        public static int GetPart1Result()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetSleepiestGuardResult(lines.ToList()).TotalSleepiest;
        }

        public static int GetPart2Result()
        {
            var lines = File.ReadAllLines(FilePath);

            return GetSleepiestGuardResult(lines.ToList()).SleepiestSameTime;
        }

        public static (int TotalSleepiest, int SleepiestSameTime) GetSleepiestGuardResult(List<string> lines)
        {
            var activities = GetSortedActivities(lines);
            var guardSleepTimes = new Dictionary<int, GuardSleepTime>();

            var fellAsleepAtMinutes = 0;
            foreach (var activity in activities)
            {
                if (activity.ActivityType == ActivityType.WentToSleep)
                {
                    fellAsleepAtMinutes = activity.EventAt.Minute;
                }

                if (activity.ActivityType == ActivityType.WokeUp)
                {
                    var sleptFor = activity.EventAt.Minute - fellAsleepAtMinutes;

                    if (guardSleepTimes.ContainsKey(activity.GuardId))
                    {
                        guardSleepTimes[activity.GuardId].TotalSleepTime += sleptFor;
                    }
                    else
                    {
                        var newGuardSleepTime = new GuardSleepTime();
                        newGuardSleepTime.TotalSleepTime = sleptFor;
                        newGuardSleepTime.MinutesSleeping = new Dictionary<int, int>();

                        guardSleepTimes.Add(activity.GuardId, newGuardSleepTime);
                    }

                    for (int i = activity.EventAt.Minute - sleptFor; i < activity.EventAt.Minute - sleptFor + sleptFor; i++)
                    {
                        if (guardSleepTimes[activity.GuardId].MinutesSleeping.ContainsKey(i))
                        {
                            guardSleepTimes[activity.GuardId].MinutesSleeping[i] += 1;
                        }
                        else
                        {
                            guardSleepTimes[activity.GuardId].MinutesSleeping[i] = 1;
                        }
                    }

                    fellAsleepAtMinutes = 0;
                }

                if (activity.ActivityType == ActivityType.CameOnDuty)
                {
                    fellAsleepAtMinutes = 0;
                }
            }

            var sleepiestGuard = guardSleepTimes.OrderByDescending(s => s.Value.TotalSleepTime).FirstOrDefault();
            var sleepiestMinute = sleepiestGuard.Value.MinutesSleeping.OrderByDescending(m => m.Value).FirstOrDefault().Key;

            var sleepiestGuardId = 0;
            var sleepiestGuardMinute = 0;
            var sleepiestGuardFrequency = 0;

            foreach (var guardSleepTime in guardSleepTimes)
            {
                foreach (var sleepTime in guardSleepTime.Value.MinutesSleeping)
                {
                    if (sleepTime.Value > sleepiestGuardFrequency)
                    {
                        sleepiestGuardFrequency = sleepTime.Value;
                        sleepiestGuardId = guardSleepTime.Key;
                        sleepiestGuardMinute = sleepTime.Key;
                    }
                }
            }

            return (sleepiestGuard.Key * sleepiestMinute, sleepiestGuardId * sleepiestGuardMinute);
        }

        private static List<Activity> GetSortedActivities(List<string> lines)
        {
            var activities = new List<Activity>();

            foreach (var line in lines)
            {
                var activity = new Activity();

                var segmented = line.Split(' ');

                var dateParts = segmented[0].Substring(1, segmented[0].Length - 1).Split('-');
                var timeParts = segmented[1].Substring(0, segmented[1].Length - 1).Split(':');
                var activityKeyPart = segmented[2];
                var guardPart = segmented[3];

                var year = int.Parse(dateParts[0]);
                var month = int.Parse(dateParts[1]);
                var day = int.Parse(dateParts[2]);

                var hour = int.Parse(timeParts[0]);
                var minute = int.Parse(timeParts[1]);

                activity.EventAt = new DateTime(year, month, day, hour, minute, 0);

                if (guardPart.StartsWith("#"))
                {
                    //guard change
                    activity.ActivityType = ActivityType.CameOnDuty;
                    activity.GuardId = int.Parse(guardPart.Substring(1, guardPart.Length - 1));
                }
                else
                {
                    //fell asleep or woke up
                    activity.ActivityType = activityKeyPart == "wakes" ? ActivityType.WokeUp : ActivityType.WentToSleep;
                }

                activities.Add(activity);
            }

            var sortedActivities = activities.OrderBy(o => o.EventAt).ToList();

            var previousGuardId = -1;
            foreach (var sortedActivity in sortedActivities)
            {
                //set the correct guard id
                if (sortedActivity.GuardId == 0)
                {
                    sortedActivity.GuardId = previousGuardId;
                }

                previousGuardId = sortedActivity.GuardId;
            }

            return sortedActivities;
        }

        private class Activity
        {
            public DateTime EventAt { get; set; }
            public int GuardId { get; set; }
            public ActivityType ActivityType { get; set; }
        }

        private enum ActivityType
        {
            WentToSleep,
            WokeUp,
            CameOnDuty
        }

        private class GuardSleepTime
        {
            public int TotalSleepTime { get; set; }
            public Dictionary<int, int> MinutesSleeping { get; set; }
        }
    }
}
