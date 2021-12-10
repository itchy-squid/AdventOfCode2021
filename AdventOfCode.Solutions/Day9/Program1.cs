using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day9
{
    public class Program1
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day9");

            var result = Solve(input);
            Console.WriteLine($"Sum of low points: {result}");
        }

        public static int Solve(IEnumerable<string> input)
        {
            int[][] heights = input
                .Select(line => line.Trim().ToCharArray().Select(c => int.Parse(c.ToString())).ToArray())
                .ToArray();

            return Enumerable.Range(0, heights.Length)
                .SelectMany(y => Enumerable.Range(0, heights[0].Length).Select(x => (x, y)))
                .Where(pt => IsLowPoint(pt.x, pt.y, heights))
                .Select(pt => heights[pt.y][pt.x] + 1)
                .Sum();
        }

        private static bool IsLowPoint(int x, int y, int[][] heights)
        {
            return (x == 0                      || heights[y][x - 1] > heights[y][x])
                && (y == 0                      || heights[y - 1][x] > heights[y][x])
                && (x == heights[0].Length - 1  || heights[y][x + 1] > heights[y][x])
                && (y == heights.Length - 1     || heights[y + 1][x] > heights[y][x]);
        }
    }
}