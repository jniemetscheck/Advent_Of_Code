using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day13
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day13.txt";

        public static int GetResultPartOne()
        {
            var input = File.ReadAllLines(FilePath).ToList();
            var schedule = GetBusSchedule(input);

            return GetWaitTime(schedule);
        }

        public static int GetWaitTime(BusSchedule schedule)
        {
            var found = false;
            var time = 0;
            var timeToStart = schedule.EarliestBusTime;

            while (!found)
            {
                foreach (var bus in schedule.Buses)
                {
                    if (timeToStart % bus.Id == 0)
                    {
                        time = (timeToStart - schedule.EarliestBusTime) * bus.Id;
                        found = true;
                        break;
                    }
                }

                timeToStart++;
            }

            return time;
        }

        public static BusSchedule GetBusSchedule(List<string> input)
        {
            var schedule = new BusSchedule { Buses = new List<Bus>(), EarliestBusTime = int.Parse(input[0]) };

            var busSplit = input[1].Split(',');

            foreach (var bus in busSplit)
            {
                if (bus != "x")
                {
                    schedule.Buses.Add(new Bus { Id = int.Parse(bus) });
                }
            }

            return schedule;
        }
    }

    public class BusSchedule
    {
        public int EarliestBusTime { get; set; }
        public List<Bus> Buses { get; set; }
    }

    public class Bus
    {
        public int Id { get; set; }
    }
}
