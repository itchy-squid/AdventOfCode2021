using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day8
{
    public class Program
    {
        public static bool Problem1(int length) => length == 2 || length == 4 || length == 3 || length == 7;

        public static void Main()
        {
            var input = Input.ReadAllLines("Day8");
            Dump(Solve(input, Problem1));
        }

        public static int Solve(IEnumerable<string> input, Func<int, bool> select)
        {
            var displayStringCountsByDisplayString = input
                .SelectMany(s => s.SplitAndClean('|').Last().SplitAndClean())
                .Select(s => new string(s.ToCharArray().OrderBy(s => s).ToArray()))
                .GroupBy(s => s)
                .ToDictionary(s => s.Key, s => s.Count());

            return displayStringCountsByDisplayString.Where(s => select(s.Key.Length)).Select(s => s.Value).Sum();
        }

        private static void Dump(int count)
        {
            Console.WriteLine($"Count: {count}");
        }
    }
}