using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Days
{
    public static class Day16
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day16.txt";

        public static long GetResultPartOne()
        {
            var input = File.ReadAllLines(FilePath).ToList();
            var ticketRuleGraph = GetTicketRuleGraph(input);

            return GetInvalidTicketCount(ticketRuleGraph);
        }

        public static long GetInvalidTicketCount(TicketRuleGraph graph)
        {
            var sum = 0;
            foreach (var nearbyTicket in graph.NearbyTickets)
            {
                foreach (var nearbyTicketValue in nearbyTicket.Values)
                {
                    var valid = false;
                    foreach (var graphTicketRule in graph.TicketRules)
                    {
                        foreach (var ticketRuleValue in graphTicketRule.Values)
                        {
                            if (nearbyTicketValue >= ticketRuleValue.Minimum && nearbyTicketValue <= ticketRuleValue.Maximum)
                            {
                                valid = true;
                                break;
                            }
                        }

                        if (valid)
                        {
                            break;
                        }
                    }

                    if (!valid)
                    {
                        sum += nearbyTicketValue;
                    }
                }
            }

            return sum;
        }

        public static TicketRuleGraph GetTicketRuleGraph(List<string> input)
        {
            var graph = new TicketRuleGraph { YourTicket = new Ticket(), TicketRules = new List<TicketRule>(), NearbyTickets = new List<Ticket>() };

            var count = 1;
            var skipYourTicketLine = true;
            var skipNearbyTicketLine = true;
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    count++;
                    continue;
                }
                if (count == 1)
                {
                    // ticket rules
                    var ticketRule = new TicketRule { Values = new List<TicketRuleValue>() };

                    var colonSplit = line.Split(':');
                    ticketRule.Name = colonSplit[0].Trim();

                    var orSplit = Regex.Split(colonSplit[1].Trim(), " or ");

                    foreach (var orItem in orSplit)
                    {
                        var dashSplit = orItem.Split('-');

                        ticketRule.Values.Add(new TicketRuleValue { Minimum = int.Parse(dashSplit[0]), Maximum = int.Parse(dashSplit[1]) });
                    }

                    graph.TicketRules.Add(ticketRule);
                }
                else if (count == 2)
                {
                    // your ticket
                    if (skipYourTicketLine)
                    {
                        skipYourTicketLine = false;
                        continue;
                    }

                    graph.YourTicket.Values = new List<int>();
                    graph.YourTicket.Values = line.Split(',').Select(int.Parse).ToList();
                }
                else
                {
                    // nearby tickets
                    if (skipNearbyTicketLine)
                    {
                        skipNearbyTicketLine = false;
                        continue;
                    }

                    graph.NearbyTickets.Add(new Ticket { Values = line.Split(',').Select(int.Parse).ToList() });
                }
            }

            return graph;
        }
    }

    public class TicketRuleGraph
    {
        public List<TicketRule> TicketRules { get; set; }
        public Ticket YourTicket { get; set; }
        public List<Ticket> NearbyTickets { get; set; }
    }

    public class TicketRule
    {
        public string Name { get; set; }
        public List<TicketRuleValue> Values { get; set; }
    }

    public class TicketRuleValue
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
    }

    public class Ticket
    {
        public List<int> Values { get; set; }
    }
}
