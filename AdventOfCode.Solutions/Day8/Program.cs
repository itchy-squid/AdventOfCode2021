using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day8
{
    public class Program
    {
        public static bool Problem1(int length) => length == 2 || length == 4 || length == 3 || length == 7;

        public static void Main()
        {
            var input = Input.ReadAllLines("Day8");

            var count = Solve(input, Problem1);
            Console.WriteLine($"Count: {count}");
            Console.WriteLine();
        }

        public static int Solve(IEnumerable<string> input, Func<IEnumerable<string>, int> calc)
        {
            var tokens = input
                .SelectMany(s => s.SplitAndClean().Where(token => !string.Equals(token, "|")));

            return calc(tokens);
        }

        public static int Problem1(IEnumerable<string> displayStrings)
        {
            var select = (string s) =>
            {
                var len = s.Length;
                return len == 2 || len == 4 || len == 3 || len == 7;
            };

            return displayStrings.Chunk(14).SelectMany(line => line.Skip(10)).Where(select).Count();
        }
    }
}