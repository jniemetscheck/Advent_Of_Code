using System.Linq;

namespace AdventOfCode2018.Days
{
    public static class Day08
    {
        public static int GetMetadataSum(string license)
        {
            var entries = license.Split(' ').Select(int.Parse).ToList(); ;

            var nodes = entries[0];
            var metadata = entries[1];
            var currentIndex = 2;

            for (int i = 0; i < nodes; i++)
            {
                var subNodes = entries[currentIndex];
                currentIndex++;

                var subMetadata = entries[currentIndex];
                currentIndex++;
            }

            return 0;
        }
    }
}
