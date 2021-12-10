using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day9
{
    public class Program2
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day9");

            var result = Solve(input);
            Console.WriteLine($"Product of basins: {result}");
        }

        public static long Solve(IEnumerable<string> input)
        {
            int[][] heights = input
                .Select(line => line.Trim().ToCharArray().Select(c => int.Parse(c.ToString())).ToArray())
                .ToArray();

            var width = heights[0].Length;
            var height = heights.Length;

            var lowPoints = Enumerable.Range(0, height)
                .SelectMany(y => Enumerable.Range(0, width).Select(x => (x, y)))
                .Where(pt => IsLowPoint(pt.x, pt.y, heights))
                .ToList();

            int[][] basinMap = Enumerable.Range(0, height)
                .Select(y => Enumerable.Repeat(-1, width).ToArray())
                .ToArray();

            foreach (var (pt, idx) in lowPoints.Select((pt, i) => (pt, i)))
            {
                DiscoverBasin(pt.x, pt.y, idx, heights, basinMap);
            }

            Dump(heights, basinMap);

            var basins = basinMap.SelectMany(row => row)
                .Where(basinNo => basinNo >= 0)
                .GroupBy(basinNo => basinNo)
                .Select(group => new { Number = group.Key, Size = group.Count()})
                .OrderByDescending(g => g.Size)
                .ToList();

            return basins.Take(3)
                .Select(basin => (long)basin.Size)
                .Aggregate((a, b) => a * b);
        }

        private static bool IsLowPoint(int x, int y, int[][] heights)
        {
            return (x == 0                      || heights[y][x - 1] > heights[y][x])
                && (y == 0                      || heights[y - 1][x] > heights[y][x])
                && (x == heights[0].Length - 1  || heights[y][x + 1] > heights[y][x])
                && (y == heights.Length - 1     || heights[y + 1][x] > heights[y][x]);
        }

        private static void DiscoverBasin(int x, int y, int basin, int[][] heights, int[][] basinMap, int direction=0)
        {
            if( x < 0 || x >= heights[0].Length ||y < 0 || y >= heights.Length) return;
            if (basinMap[y][x] >= 0) return;

            if (IsInBasin(x, y, basin, heights, basinMap))
            {
                basinMap[y][x] = basin;

                DiscoverBasin(x - 1, y, basin, heights, basinMap);
                DiscoverBasin(x, y - 1, basin, heights, basinMap);
                DiscoverBasin(x + 1, y, basin, heights, basinMap);
                DiscoverBasin(x, y + 1, basin, heights, basinMap);
            }
        }

        private static bool IsInBasin(int x, int y, int basin, int[][] heights, int[][] basinMap)
        {
            return heights[y][x] != 9
                && (x == 0                     || heights[y][x - 1] >= heights[y][x] || basinMap[y][x-1] == basin)
                && (y == 0                     || heights[y - 1][x] >= heights[y][x] || basinMap[y-1][x] == basin)
                && (x == heights[0].Length - 1 || heights[y][x + 1] >= heights[y][x] || basinMap[y][x + 1] == basin)
                && (y == heights.Length - 1    || heights[y + 1][x] >= heights[y][x] || basinMap[y+1][x] == basin);
        }

        private static void Dump(int[][]heights, int[][] basinMap)
        {
            ConsoleColor[] colors = new[]
            {
                ConsoleColor.DarkBlue,
                ConsoleColor.DarkCyan,
                ConsoleColor.DarkGray,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkMagenta,
                ConsoleColor.DarkRed,
                ConsoleColor.DarkYellow
            };

            var chooseColor = (int basin) => basin < 0 ? ConsoleColor.Black : colors[basin % colors.Length];

            for(int y = 0; y < heights.Length; y++)
            {
                for(int x = 0; x < heights[0].Length; x++)
                {
                    Console.BackgroundColor = chooseColor(basinMap[y][x]);
                    Console.Write(heights[y][x]);
                }
                Console.BackgroundColor = chooseColor(-1);
                Console.WriteLine();
            }
        }
    }
}