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
                return new[] { 2, 4, 3, 7 }.Contains(len);
            };

            return input
                .Tokenize()
                .Where(token => !string.Equals(token, "|"))
                .Chunk(14)
                .SelectMany(line => line.Skip(10))
                .Where(select)
                .Count();
        }
    }
}