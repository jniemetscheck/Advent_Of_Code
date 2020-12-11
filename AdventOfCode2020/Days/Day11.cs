using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Days
{
    public static class Day11
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day11.txt";

        public static double GetResultPartOne()
        {
            var input = File.ReadAllLines(FilePath).ToList();
            var seatingChart = GetMappedSeatingChart(input);

            return GetNumberOfOccupiedSeats(seatingChart);
        }

        public static List<List<Seat>> GetMappedSeatingChart(List<string> input)
        {
            var seatingChart = new List<List<Seat>>();

            foreach (var item in input)
            {
                var row = new List<Seat>();
                foreach (var s in item)
                {
                    row.Add(new Seat
                    {
                        Status = s.ToString() == "." ? SeatStatus.Floor : s.ToString() == "L" ? SeatStatus.Empty : SeatStatus.Occupied
                    });
                }

                seatingChart.Add(row);
            }

            return seatingChart;
        }

        public static int GetNumberOfOccupiedSeats(List<List<Seat>> seatingChart)
        {
            while (true)
            {
                var newSeatingChart = new List<List<Seat>>();
                var hasChanged = false;

                for (var i = 0; i < seatingChart.Count; i++)
                {
                    //add new row
                    newSeatingChart.Add(new List<Seat>());

                    for (int j = 0; j < seatingChart[i].Count; j++)
                    {
                        //add new seat
                        newSeatingChart[i].Add(new Seat { Status = seatingChart[i][j].Status });

                        if (seatingChart[i][j].Status == SeatStatus.Empty && !HasAdjacentOccupiedSeats(seatingChart, i, j))
                        {
                            newSeatingChart[i][j].Status = SeatStatus.Occupied;
                            hasChanged = true;
                        }
                        else if (seatingChart[i][j].Status == SeatStatus.Occupied && HasExceededAdjecentSeatOccupancyLimit(seatingChart, i, j))
                        {
                            newSeatingChart[i][j].Status = SeatStatus.Empty;
                            hasChanged = true;
                        }
                    }
                }

                if (!hasChanged)
                {
                    break;
                }

                seatingChart = newSeatingChart;

                //WriteOutSeatingChart(seatingChart);
            }

            return GetOccupiedSeatCount(seatingChart);
        }

        public static bool HasAdjacentOccupiedSeats(List<List<Seat>> seatingChart, int currentRow, int currentSeat)
        {
            var hasAdjacentOccupiedSeats = false;

            for (var i = currentRow - 1; i <= currentRow + 1; i++)
            {
                for (var j = currentSeat - 1; j <= currentSeat + 1; j++)
                {
                    if (seatingChart.ElementAtOrDefault(i) != null)
                    {
                        if (seatingChart[i].ElementAtOrDefault(j) != null)
                        {
                            if (seatingChart[i][j].Status == SeatStatus.Occupied)
                            {
                                hasAdjacentOccupiedSeats = true;
                                break;
                            }
                        }
                    }
                }

                if (hasAdjacentOccupiedSeats)
                {
                    break;
                }
            }

            return hasAdjacentOccupiedSeats;
        }

        public static bool HasExceededAdjecentSeatOccupancyLimit(List<List<Seat>> seatingChart, int currentRow, int currentSeat)
        {
            var occupiedSeatCount = 0;

            for (var i = currentRow - 1; i <= currentRow + 1; i++)
            {
                if (seatingChart.ElementAtOrDefault(i) != null)
                {
                    for (var j = currentSeat - 1; j <= currentSeat + 1; j++)
                    {
                        if (seatingChart[i].ElementAtOrDefault(j) != null)
                        {
                            if (!(i == currentRow && j == currentSeat) && seatingChart[i][j].Status == SeatStatus.Occupied)
                            {
                                occupiedSeatCount++;
                            }
                        }
                    }
                }
            }

            return occupiedSeatCount >= 4;
        }

        public static int GetOccupiedSeatCount(List<List<Seat>> seatingChart)
        {
            var occupiedSeatCount = 0;

            foreach (var row in seatingChart)
            {
                foreach (var seat in row)
                {
                    if (seat.Status == SeatStatus.Occupied)
                    {
                        occupiedSeatCount++;
                    }
                }
            }

            return occupiedSeatCount;
        }

        public static void WriteOutSeatingChart(List<List<Seat>> seatingChart)
        {
            foreach (var row in seatingChart)
            {
                var rowString = new StringBuilder();

                foreach (var seat in row)
                {
                    if (seat.Status == SeatStatus.Floor)
                    {
                        rowString.Append(".");
                    }
                    else if (seat.Status == SeatStatus.Empty)
                    {
                        rowString.Append("L");
                    }
                    else
                    {
                        rowString.Append("#");
                    }
                }

                Console.WriteLine(rowString);
            }
            Console.WriteLine();
        }
    }

    public class Seat
    {
        public SeatStatus Status { get; set; }
    }

    public enum SeatStatus
    {
        Occupied,
        Floor,
        Empty
    }
}
