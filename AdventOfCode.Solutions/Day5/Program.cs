using AdventOfCode.Solutions.Services;

namespace AdventOfCode.Solutions.Day5
{
    public class Program
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day5");
            var result = Solve(input);
            Console.Write($"Number of dangerous points: {result}");
        }

        public static int Solve(IEnumerable<string> input)
        {
            var lines = input.Select(line => Line.Parse(line)).Where(line => line.IsVertical || line.IsHorizontal);
            var field = new Field(lines);

            return Enumerable
                .Range(field.Start.X, field.Width)
                .SelectMany(x => Enumerable.Range(field.Start.Y, field.Height).Select(y => new Point(x, y)))
                .Where(pt => field.Read(pt.X, pt.Y) >= 2)
                .Count();
        }
    }
}
