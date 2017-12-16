using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public static class Day13
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day13.txt";

        public static int GetPart1Result()
        {
            return GetResult(GetInput(), 0, true);
        }

        public static int GetResultTwo(List<FirewallEntry> firewallEntries)
        {
            var picosecond = 0;
            firewallEntries = GetFilledFirewallEntries(firewallEntries);
            while (true)
            {
                var result = GetResult(firewallEntries, picosecond, false);

                if (result == 0)
                {
                    break;
                }

                picosecond++;
            }

            return picosecond;
        }

        public static int GetResult(List<FirewallEntry> firewallEntries, int picosecondToStartAt, bool fillInEntries)
        {
            var picosecond = 0;
            var filledFirewallEntries = fillInEntries ? GetFilledFirewallEntries(SetInitialState(firewallEntries)) : firewallEntries;
            var currentDepth = 0 - picosecondToStartAt;
            var caught = new List<FirewallEntry>();
            var severity = 0;

            severity = 0;

            for (int i = 1; i <= filledFirewallEntries.Count + picosecondToStartAt; i++)
            {
                foreach (var scannerAtBeginningEntry in filledFirewallEntries.Where(x => x.ScannerAtRange == 1 && x.Range > 0))
                {
                    if (scannerAtBeginningEntry.Depth == currentDepth)
                    {
                        //we've been caught
                        caught.Add(scannerAtBeginningEntry);
                    }
                }
                foreach (var entry in filledFirewallEntries)
                {
                    if (entry.Range > 0)
                    {
                        if (entry.MovingDown)
                        {
                            if (entry.ScannerAtRange < entry.Range)
                            {
                                entry.ScannerAtRange++;
                            }
                            else if (entry.ScannerAtRange == entry.Range)
                            {
                                entry.ScannerAtRange--;
                                entry.MovingDown = false;
                            }
                        }
                        else
                        {
                            if (entry.ScannerAtRange > 1)
                            {
                                entry.ScannerAtRange--;
                            }
                            else if (entry.ScannerAtRange == 1)
                            {
                                entry.ScannerAtRange++;
                                entry.MovingDown = true;
                            }
                        }
                    }
                }
                picosecond++;

                currentDepth++;
            }

            foreach (var caughtEntry in caught)
            {
                severity += caughtEntry.Depth * caughtEntry.Range;
            }

            return severity;
        }

        private static List<FirewallEntry> SetInitialState(List<FirewallEntry> firewallEntries)
        {
            foreach (var entry in firewallEntries)
            {
                entry.ScannerAtRange = 1;
            }

            return firewallEntries;
        }

        private static List<FirewallEntry> GetFilledFirewallEntries(List<FirewallEntry> firewallEntries)
        {
            for (int i = 0; i < firewallEntries.OrderBy(f => f.Depth).LastOrDefault().Depth; i++)
            {
                if (firewallEntries.FirstOrDefault(e => e.Depth == i) == null)
                {
                    var filler = new FirewallEntry { Range = 0 };
                    firewallEntries.Insert(i, filler);
                }
            }

            return firewallEntries;
        }

        private static List<FirewallEntry> GetInput()
        {
            var entries = new List<FirewallEntry>();

            var file = new StreamReader(FilePath);
            string line;

            while ((line = file.ReadLine()) != null)
            {
                var lineSplit = line.Split(':');
                var depth = int.Parse(lineSplit[0].Trim());
                var range = int.Parse(lineSplit[1].Trim());
                entries.Add(new FirewallEntry { Depth = depth, Range = range });
            }

            return entries;
        }
    }

    public class FirewallEntry
    {
        public int Depth { get; set; }
        public int Range { get; set; }
        public int ScannerAtRange { get; set; }
        public bool MovingDown { get; set; }

        public FirewallEntry()
        {
            MovingDown = true;
        }
    }
}
