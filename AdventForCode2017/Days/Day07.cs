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

            return lowestFloor;
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
        }
    }
}
