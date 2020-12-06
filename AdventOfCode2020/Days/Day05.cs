using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day05
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day05.txt";

        public static int GetResultPartOne()
        {
            var input = File.ReadAllLines(FilePath);

            var seatAssignments = ParseSeatAssignments(input.ToList());
            SetSeatIds(seatAssignments);

            return seatAssignments.Max(s => s.SeatId);
        }

        public static int GetResultPartTwo()
        {
            var input = File.ReadAllLines(FilePath);

            var seatAssignments = ParseSeatAssignments(input.ToList());
            SetSeatIds(seatAssignments);

            seatAssignments = seatAssignments.OrderBy(o => o.SeatId).ToList();

            var currentSeatId = seatAssignments[0].SeatId - 1;

            foreach (var seatAssignment in seatAssignments)
            {
                if (seatAssignment.SeatId != currentSeatId + 1)
                {
                    currentSeatId += 1;
                    break;
                }

                currentSeatId = seatAssignment.SeatId;
            }

            return currentSeatId;
        }

        public static List<SeatAssignment> ParseSeatAssignments(List<string> unparsedSeatAssigments)
        {
            var result = new List<SeatAssignment>();

            foreach (var unparsedSeatAssigment in unparsedSeatAssigments)
            {
                result.Add(new SeatAssignment
                {
                    Rows = unparsedSeatAssigment.Substring(0, 7).ToUpper(),
                    Columns = unparsedSeatAssigment.Substring(7, 3).ToUpper()
                });
            }

            return result;
        }

        public static void SetSeatIds(List<SeatAssignment> seatAssignments)
        {
            foreach (var seatAssignment in seatAssignments)
            {
                //figure out seat assignment 
                var minRow = 0;
                var maxRow = 127;
                foreach (var seatAssignmentRow in seatAssignment.Rows)
                {
                    var difference = maxRow - minRow + 1;
                    if (seatAssignmentRow == 'F')
                    {
                        maxRow = (minRow + difference / 2) - 1;
                    }
                    else
                    {
                        minRow = (minRow + difference / 2);
                    }
                }

                var minColumn = 0;
                var maxColumn = 7;
                foreach (var seatAssignmentColumn in seatAssignment.Columns)
                {
                    var difference = maxColumn - minColumn + 1;
                    if (seatAssignmentColumn == 'L')
                    {
                        maxColumn = (minColumn + difference / 2) - 1;
                    }
                    else
                    {
                        minColumn = (minColumn + difference / 2);
                    }
                }

                seatAssignment.Row = minRow;
                seatAssignment.Column = maxColumn;

                //calculate seat id
                seatAssignment.SeatId = seatAssignment.Row * 8 + seatAssignment.Column;
            }
        }
    }

    public class SeatAssignment
    {
        public int SeatId { get; set; }
        public string Rows { get; set; }
        public string Columns { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
