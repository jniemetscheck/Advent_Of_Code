using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public static class Day03
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day03.txt";

        public static (int OverlappingCount, int NonOverlappingClaimId) GetResult()
        {
            var lines = File.ReadAllLines(FilePath);
            var result = GetResult(lines.ToList());

            return (result.OverlappingCount, result.NonOverlappingClaimId);
        }

        public static (int OverlappingCount, int NonOverlappingClaimId) GetResult(List<string> lines)
        {
            var claims = GetClaims(lines);
            var grid = GetInitiatedGrid(1000, 1000);
            var overlappingClaims = new List<Claim>();
            var nonOverlappingClaim = new Claim();

            foreach (var claim in claims)
            {
                Console.WriteLine("Claim: " + claim.Id + " of " + claims.Count);
                for (int i = claim.RowStart; i < claim.RowStart + claim.Height; i++)
                {
                    for (int j = claim.ColumnStart; j < claim.ColumnStart + claim.Width; j++)
                    {
                        if (grid.FirstOrDefault(item => item.Key.Column == j && item.Key.Row == i).Value.Count == 1)
                        {
                            if (!overlappingClaims.Contains(grid.FirstOrDefault(item => item.Key.Column == j && item.Key.Row == i).Value[0]))
                            {
                                overlappingClaims.Add(grid.FirstOrDefault(item => item.Key.Column == j && item.Key.Row == i).Value[0]);
                            }

                            if (!overlappingClaims.Contains(claim))
                            {
                                overlappingClaims.Add(claim);
                            }
                        }
                        grid.FirstOrDefault(item => item.Key.Column == j && item.Key.Row == i).Value.Add(claim);
                    }
                }
            }

            foreach (var claim in claims)
            {
                if (!overlappingClaims.Contains(claim))
                {
                    nonOverlappingClaim = claim;
                    break;
                }
            }

            return (grid.Count(g => g.Value.Count > 1), nonOverlappingClaim.Id);
        }

        private static Dictionary<Coordinate, List<Claim>> GetInitiatedGrid(int columns, int rows)
        {
            var result = new Dictionary<Coordinate, List<Claim>>();

            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    result.Add(new Coordinate{Row = j, Column = i}, new List<Claim>());
                }
            }

            return result;
        }

        private static List<Claim> GetClaims(List<string> lines)
        {
            var claims = new List<Claim>();

            foreach (var line in lines)
            {
                //#1 @ 1,3: 4x4
                var claim = new Claim();

                var segmentSplit = line.Split(' ');

                claim.Id = int.Parse(segmentSplit[0].Substring(1, segmentSplit[0].Length - 1));

                var coordinates = segmentSplit[2].Split(',');
                claim.ColumnStart = int.Parse(coordinates[0]);
                claim.RowStart = int.Parse(coordinates[1].Substring(0, coordinates[1].Length - 1));

                var area = segmentSplit[3].Split('x');
                claim.Width = int.Parse(area[0]);
                claim.Height = int.Parse(area[1]);

                claims.Add(claim);
            }

            return claims;
        }

        private class Claim
        {
            public int Id { get; set; }
            public int ColumnStart { get; set; }
            public int RowStart { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }

        private class Coordinate
        {
            public int Column { get; set; }
            public int Row { get; set; }
        }
    }
}
