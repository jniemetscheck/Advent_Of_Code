using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public static class Day07
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day07.txt";

        public static string GetPart1Result()
        {
            return GetResult().Bottom;
        }

        public static int GetPart2Result()
        {
            var results = GetResult();
            var tower = results.Tower;

            return DoBalanceCheck(tower, results.Bottom).WeightNeeded;
        }

        private static (bool IsImbalanced, int WeightNeeded) DoBalanceCheck(Tower tower, string bottomName)
        {
            var bottom = tower.Floors.FirstOrDefault(x => x.Name == bottomName);
            var weights = new Dictionary<int, List<string>>();

            if (bottom.FloorsAbove != null && bottom.FloorsAbove.Any())
            {
                foreach (var floor in bottom.FloorsAbove)
                {
                    var currentFloor = tower.Floors.FirstOrDefault(x => x.Name == floor.Name);

                    var floorsAboveWeight = GetWeight(tower, floor.Name);

                    if (!weights.ContainsKey(currentFloor.Weight + floorsAboveWeight))
                    {
                        weights.Add(currentFloor.Weight + floorsAboveWeight, new List<string>() { currentFloor.Name });
                    }
                    else
                    {
                        weights[currentFloor.Weight + floorsAboveWeight].Add(currentFloor.Name);
                    }
                }

                if (weights.Count > 1 && weights.Any(x => x.Value.Count == 1))
                {
                    //imbalanced
                    var goodWeight = weights.FirstOrDefault(y => y.Value.Count > 1).Key;

                    if (goodWeight > bottom.Weight)
                    {
                        //we're under weight
                        bottom.Weight = bottom.Weight + (goodWeight - bottom.Weight);
                    }
                    else
                    {
                        //we're over weight
                        bottom.Weight = bottom.Weight - (bottom.Weight - goodWeight);
                    }

                    //if we're bad, we need to check to see which one is actually imbalanced - recursion

                    return DoBalanceCheck(tower, weights.FirstOrDefault(y => y.Value.Count == 1).Value[0]);
                }
                else
                {
                    foreach (var floor in bottom.FloorsAbove)
                    {
                        var check = DoBalanceCheck(tower, floor.Name);

                        if (check.IsImbalanced)
                        {
                            return (true, check.WeightNeeded);
                        }
                    }
                }
            }
            else
            {
                return (false, 0);
            }

            return (true, 0);
        }

        private static int GetWeight(Tower tower, string bottomName)
        {
            var currentFloor = tower.Floors.FirstOrDefault(x => x.Name == bottomName);
            var totalWeight = 0;

            if (currentFloor.FloorsAbove != null && currentFloor.FloorsAbove.Any())
            {
                foreach (var floorAbove in currentFloor.FloorsAbove)
                {
                    var floor = tower.Floors.FirstOrDefault(x => x.Name == floorAbove.Name);
                    totalWeight += GetWeight(tower, floorAbove.Name) + floor.Weight;
                }
            }

            return totalWeight;
        }

        private static (string Bottom, Tower Tower) GetResult()
        {
            var lowestFloor = string.Empty;
            var tower = GetTower();
            var floorsWithOtherFloors = tower.Floors.Where(f => f.FloorsAbove != null && f.FloorsAbove.Count > 0);

            foreach (var floor in floorsWithOtherFloors)
            {
                var outerExists = false;
                foreach (var innerFloor in floorsWithOtherFloors)
                {

                    //skip current floor
                    if (innerFloor.Name != floor.Name)
                    {
                        var exists = false;
                        foreach (var innerFloorsFloors in innerFloor.FloorsAbove)
                        {
                            if (innerFloorsFloors.Name == floor.Name)
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (exists)
                        {
                            outerExists = true;
                            break;
                        }
                    }
                }
                if (!outerExists)
                {
                    lowestFloor = floor.Name;
                }
            }

            return (lowestFloor, tower);
        }


        private static Tower GetTower()
        {
            var tower = new Tower();
            tower.Floors = new List<Floor>();

            var file = new StreamReader(FilePath);
            string line;

            while ((line = file.ReadLine()) != null)
            {
                var floorsSplit = line.Split("->".ToCharArray());
                var floorDetailSplit = floorsSplit[0].Split('(');
                var name = floorDetailSplit[0].Trim();
                var weight = floorDetailSplit[1].Trim().TrimEnd(')').Trim();

                var floor = new Floor { Name = name, Weight = int.Parse(weight) };

                if (floorsSplit.Length > 1)
                {
                    //has floors above it
                    var floorsAbove = new List<Floor>();
                    var floorsAboveSplit = floorsSplit[2].Split(',');
                    foreach (var floorAbove in floorsAboveSplit)
                    {
                        floorsAbove.Add(new Floor { Name = floorAbove.Trim() });
                    }
                    floor.FloorsAbove = floorsAbove;
                }

                tower.Floors.Add(floor);
            }

            return tower;
        }

        private class Tower
        {
            public List<Floor> Floors { get; set; }
        }

        private class Floor
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public List<Floor> FloorsAbove { get; set; }
            //public int TotalWeight { get; set; }
        }
    }
}
