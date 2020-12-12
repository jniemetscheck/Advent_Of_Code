using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

            return GetNumberOfOccupiedSeats(seatingChart, HasNoAdjacentOccupiedSeats, 4);
        }

        public static double GetResultPartTwo()
        {
            var input = File.ReadAllLines(FilePath).ToList();
            var seatingChart = GetMappedSeatingChart(input);

            return GetNumberOfOccupiedSeats(seatingChart, HasNoAdjacentOccupiedSeatsExtended, 5);
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

        public static int GetNumberOfOccupiedSeats(List<List<Seat>> seatingChart, Func<List<List<Seat>>, int, int, bool> occupancyCheckFunc, int limit)
        {
            while (true)
            {
                var newSeatingChart = new List<List<Seat>>();
                var hasChanged = false;

                for (var i = 0; i < seatingChart.Count; i++)
                {
                    //add new row
                    newSeatingChart.Add(new List<Seat>());

                    for (var j = 0; j < seatingChart[i].Count; j++)
                    {
                        //add new seat
                        newSeatingChart[i].Add(new Seat { Status = seatingChart[i][j].Status });

                        if (seatingChart[i][j].Status == SeatStatus.Empty && occupancyCheckFunc(seatingChart, i, j))
                        {
                            newSeatingChart[i][j].Status = SeatStatus.Occupied;
                            hasChanged = true;
                        }
                        else if (seatingChart[i][j].Status == SeatStatus.Occupied && HasExceededAdjacentSeatOccupancyLimit(seatingChart, i, j, limit))
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

                WriteOutSeatingChart(seatingChart);
            }

            return GetOccupiedSeatCount(seatingChart);
        }

        public static bool HasNoAdjacentOccupiedSeats(List<List<Seat>> seatingChart, int currentRow, int currentSeat)
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

            return !hasAdjacentOccupiedSeats;
        }

        public static bool HasNoAdjacentOccupiedSeatsExtended(List<List<Seat>> seatingChart, int currentRow, int currentSeat)
        {
            var occupiedSeatCount = 0;

            //front
            for (int f = 0; f < currentRow; f++)
            {
                if (seatingChart[f][currentSeat].Status == SeatStatus.Empty)
                {
                    break;
                }
                if (seatingChart[f][currentSeat].Status == SeatStatus.Occupied)
                {
                    occupiedSeatCount++;
                    break;
                }
            }

            //left
            for (int l = 0; l < currentSeat; l++)
            {
                if (seatingChart[currentRow][l].Status == SeatStatus.Empty)
                {
                    break;
                }
                if (seatingChart[currentRow][l].Status == SeatStatus.Occupied)
                {
                    occupiedSeatCount++;
                    break;
                }
            }

            //right
            for (int r = currentSeat + 1; r < seatingChart[currentRow].Count; r++)
            {
                if (seatingChart[currentRow][r].Status == SeatStatus.Empty)
                {
                    break;
                }
                if (seatingChart[currentRow][r].Status == SeatStatus.Occupied)
                {
                    occupiedSeatCount++;
                    break;
                }
            }

            //behind
            for (int b = currentRow + 1; b < seatingChart.Count; b++)
            {
                if (seatingChart[b][currentSeat].Status == SeatStatus.Empty)
                {
                    break;
                }
                if (seatingChart[b][currentSeat].Status == SeatStatus.Occupied)
                {
                    occupiedSeatCount++;
                    break;
                }
            }


            //diag front left
            var thisSeat = currentSeat - 1;
            for (int fl = currentRow - 1; fl > 0; fl--)
            {
                if (seatingChart[fl].ElementAtOrDefault(thisSeat) == null || seatingChart[fl][thisSeat].Status == SeatStatus.Empty)
                {
                    break;
                }

                if (seatingChart[fl][thisSeat].Status == SeatStatus.Occupied)
                {
                    occupiedSeatCount++;
                    break;
                }
                thisSeat--;
            }

            return occupiedSeatCount < 5;
        }

        public static bool HasExceededAdjacentSeatOccupancyLimit(List<List<Seat>> seatingChart, int currentRow, int currentSeat, int limit)
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

            return occupiedSeatCount >= limit;
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
