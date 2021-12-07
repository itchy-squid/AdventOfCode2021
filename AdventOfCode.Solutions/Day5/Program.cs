using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day5
{
    public class Program
    {
        public static readonly Func<Line, bool> Problem1 = l => l.IsVertical || l.IsHorizontal;
        public static readonly Func<Line, bool> Problem2 = l => true;

        public static void Main()
        {
            int result;
            var input = Input.ReadAllLines("Day5");
            
            result = Solve(input, Problem1);
            Dump(result);

            result = Solve(input, Problem2);
            Dump(result);
        }

        public static int Solve(IEnumerable<string> input, Func<Line, bool> pick)
        {
            var lines = input.Select(line => Line.Parse(line)).Where(pick);
            var field = new Field(lines);

            return field.Points().Where(p => p.Count >= 2).Count();
        }

        public static void Dump(int result)
        {
            Console.Write($"Number of dangerous points: {result}");
            Console.WriteLine();
        }
    }
}
