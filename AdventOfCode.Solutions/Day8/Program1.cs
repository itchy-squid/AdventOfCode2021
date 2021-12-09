using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day8
{
    public class Program1
    {
        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            var input = await Input.ReadAllTextAsync("Day8");

            var count = Solve(input);
            Console.WriteLine($"Count: {count}");
            Console.WriteLine();
        }

        public static int Solve(string input)
        {
            var select = (string s) =>
            {
                var len = s.Length;
                return len == 2 || len == 4 || len == 3 || len == 7;
            };

            return input
                .SplitAndClean()
                .Where(token => !string.Equals(token, "|"))
                .Chunk(14)
                .SelectMany(line => line.Skip(10))
                .Where(select)
                .Count();
        }
    }
}