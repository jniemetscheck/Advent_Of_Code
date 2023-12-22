using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2023.Days
{
    public static class Day02
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day02.txt";

        public static int GetResultPartOne()
        {
            var lines = File.ReadAllLines(FilePath);
            var games = GetMappedGames(lines.ToList());

            return GetPossibleGameCount(games, 12, 13, 14);
        }

        public static double GetResultPartTwo()
        {
            var lines = File.ReadAllLines(FilePath);
            var games = GetMappedGames(lines.ToList());

            return GetMinimumCubePowerSum(games);
        }

        public static List<Game> GetMappedGames(List<string> input)
        {
            var result = new List<Game>();

            foreach (var line in input)
            {
                var gameIdSplit = line.Split(':');

                var game = new Game { Id = int.Parse(gameIdSplit[0].Split(' ')[1]), Sets = new List<Set>() };

                var setSplit = gameIdSplit[1].Split(';');

                foreach (var setSplitItem in setSplit)
                {
                    var set = new Set { Combinations = new List<Combination>() };

                    var combinationSplit = setSplitItem.Split(',');

                    foreach (var combinationSplitItem in combinationSplit)
                    {
                        var combination = new Combination();

                        var blockSplit = combinationSplitItem.Trim().Split(' ');

                        combination.Count = int.Parse(blockSplit[0]);
                        combination.Color = blockSplit[1].ToLower() == "blue" ? BlockColor.Blue : (blockSplit[1].ToLower() == "green" ? BlockColor.Green : BlockColor.Red);

                        set.Combinations.Add(combination);
                    }

                    game.Sets.Add(set);
                }

                result.Add(game);
            }

            return result;
        }

        public static int GetPossibleGameCount(List<Game> games, int redMax, int greenMax, int blueMax)
        {
            var result = 0;

            foreach (var game in games)
            {
                var isGamePossible = true;
                foreach (var set in game.Sets)
                {
                    foreach (var combination in set.Combinations)
                    {
                        switch (combination.Color)
                        {
                            case BlockColor.Red:
                                if (combination.Count > redMax)
                                {
                                    isGamePossible = false;
                                }
                                break;
                            case BlockColor.Green:
                                if (combination.Count > greenMax)
                                {
                                    isGamePossible = false;
                                }
                                break;
                            case BlockColor.Blue:
                                if (combination.Count > blueMax)
                                {
                                    isGamePossible = false;
                                }
                                break;
                        }
                    }
                }

                if (isGamePossible)
                {
                    Console.WriteLine(game.Id);
                    result += game.Id;
                }
            }

            return result;
        }

        public static double GetMinimumCubePowerSum(List<Game> games)
        {
            var result = 0d;

            foreach (var game in games)
            {
                var minimumRedCount = 0;
                var minimumGreenCount = 0;
                var minimumBlueCount = 0;

                foreach (var set in game.Sets)
                {
                    foreach (var combination in set.Combinations)
                    {
                        switch (combination.Color)
                        {
                            case BlockColor.Red:
                                if (minimumRedCount <= combination.Count)
                                {
                                    minimumRedCount = combination.Count;
                                }
                                break;
                            case BlockColor.Green:
                                if (minimumGreenCount <= combination.Count)
                                {
                                    minimumGreenCount = combination.Count;
                                }
                                break;
                            case BlockColor.Blue:
                                if (minimumBlueCount <= combination.Count)
                                {
                                    minimumBlueCount = combination.Count;
                                }
                                break;
                        }
                    }
                }

                game.Power = minimumRedCount * minimumGreenCount * minimumBlueCount;
            }



            return games.Sum(s => s.Power);
        }
    }

    public class Game
    {
        public int Id { get; set; }
        public List<Set> Sets { get; set; }
        public double Power { get; set; }
    }

    public class Set
    {
        public List<Combination> Combinations { get; set; }
    }

    public class Combination
    {
        public int Count { get; set; }
        public BlockColor Color { get; set; }
    }

    public enum BlockColor
    {
        Blue,
        Green,
        Red
    }
}