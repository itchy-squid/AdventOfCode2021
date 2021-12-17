using AdventOfCode.Solutions.Tools;
using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day15
{
    public static class Program
    {
        public const int Problem1 = 1;
        public const int Problem2 = 5;

        public static void Main()
        {
            var input = Input.ReadAllLines("Day15");

            var result1 = Solve(input, Problem1);
            Console.WriteLine(result1);
            Console.WriteLine();

            var result2 = Solve(input, Problem2);
            Console.WriteLine(result2);
            Console.WriteLine();
        }

        public static long Solve(IEnumerable<string> lines, int scale)
        {
            var model = Parse(lines.ToImmutableList(), scale);
            var path = Djikstra(model);
            return path.Last().Item2;
        }

        private static int[,] Parse(IImmutableList<string> lines, int scale)
        {
            var width = lines.GroupBy(x => x.Length).Select(x => x.Key).Single();
            var height = lines.Count;
            var result = new int[height * scale, width * scale];

            foreach (var i in Enumerable.Range(0, scale * scale))
            {
                var blockXOffset = (i / scale);
                var blockYOffset = (i % scale);

                BlockCopy(lines, result, 
                    blockXOffset * width, 
                    blockYOffset * height, 
                    blockXOffset + blockYOffset);
            }

            return result;
        }

        private static int[,] BlockCopy(IImmutableList<string> lines, int[,] result, int xOffset, int yOffset, int bonus)
        {
            var width = lines.GroupBy(x => x.Length).Select(x => x.Key).Single();
            var height = lines.Count;

            foreach (var (line, y) in lines.Select((line, y) => (line, y)))
            {
                for (var x = 0; x < line.Length; x++)
                {
                    result[y + yOffset, x + xOffset] = line[x].ToInt() + bonus;
                }
            }

            return result;
        }


        // Stolen from Wikipedia
        private static Stack<((int, int), int)> Djikstra(int[,] model)
        {
            int height = model.GetLength(1);
            int width = model.GetLength(0);

            HashSet<(int, int)> Q = new HashSet<(int, int)>();
            int[,] distances = new int[height, width];
            (int, int)?[,] prev = new (int, int)?[height, width];

            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    distances[y, x] = int.MaxValue;
                    prev[y, x] = null;
                    Q.Add((x, y));
                }
            }

            distances[0,0] = 0;

            while (Q.Any())
            {
                (int, int) u = Q.MinBy(v => distances[v.Item2, v.Item1]);
                Q.Remove(u);

                if(u.Item1 == width - 1 && u.Item2 == height - 1)
                {
                    break;
                }

                var neighbors = new[] { (u.Item1 - 1, u.Item2), (u.Item1 + 1, u.Item2), (u.Item1, u.Item2 - 1), (u.Item1, u.Item2 + 1) };
                foreach (var v in neighbors.Where(n => Q.Contains(n)))
                {
                    var alt = distances[u.Item2, u.Item1] + model[v.Item2,v.Item1];
                    if(alt < distances[v.Item2, v.Item1])
                    {
                        distances[v.Item2, v.Item1] = alt;
                        prev[v.Item2, v.Item1] = u;
                    }
                }
            }

            Stack<((int, int), int)> path = new();
            (int, int)? u2 = (height - 1, width - 1);
            while(u2 != null)
            {
                path.Push((u2.Value, distances[u2.Value.Item2, u2.Value.Item1]));
                u2 = prev[u2.Value.Item2, u2.Value.Item1];
            }

            return path;
        }
    }
}
