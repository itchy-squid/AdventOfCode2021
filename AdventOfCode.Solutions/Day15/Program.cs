using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day15
{
    public static class Program
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day15");

            var result1 = Solve(input);
            Console.WriteLine(result1);
            Console.WriteLine();

            //var result2 = Solve(input, 40);
            //Console.WriteLine(result2);
            //Console.WriteLine();
        }

        public static long Solve(IEnumerable<string> lines)
        {
            var model = Parse(lines);
            var path = Djikstra(model);
            return path.Last().Item2;
        }

        private static int[][] Parse(IEnumerable<string> lines)
        {
            return lines
                .Select(line => line.Select(ch => ch.ToInt()).ToArray())
                .ToArray();
        }

        // Stolen from Wikipedia
        private static Stack<((int, int), int)> Djikstra(int[][] model)
        {
            int rows = model.Length;
            int cols = model[0].Length;

            HashSet<(int, int)> Q = new HashSet<(int, int)>();
            int[,] distances = new int[rows, cols];
            (int, int)?[,] prev = new (int, int)?[model.Length, model[0].Length];

            for (int y = 0; y < model.Length; y++)
            {
                for(int x = 0; x < model[y].Length; x++)
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

                if(u.Item1 == cols - 1 && u.Item2 == rows - 1)
                {
                    break;
                }

                var neighbors = new[] { (u.Item1 - 1, u.Item2), (u.Item1 + 1, u.Item2), (u.Item1, u.Item2 - 1), (u.Item1, u.Item2 + 1) };
                foreach (var v in neighbors.Where(n => Q.Contains(n)))
                {
                    var alt = distances[u.Item2, u.Item1] + model[v.Item2][v.Item1];
                    if(alt < distances[v.Item2, v.Item1])
                    {
                        distances[v.Item2, v.Item1] = alt;
                        prev[v.Item2, v.Item1] = u;
                    }
                }
            }

            Stack<((int, int), int)> path = new();
            (int, int)? u2 = (rows - 1, cols - 1);
            while(u2 != null)
            {
                path.Push((u2.Value, distances[u2.Value.Item2, u2.Value.Item1]));
                u2 = prev[u2.Value.Item2, u2.Value.Item1];
            }

            return path;
        }
    }
}
