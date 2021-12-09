using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day8
{
    public class Program2
    {
        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            var input = await Input.ReadAllTextAsync("Day8");

            var count = Solve(input);
            Console.WriteLine($"Count: {count}");
            Console.WriteLine();
        }

        public static int Solve(string input)
        {
            return input.SplitLines().Select(line => SolveLine(line)).Sum();
        }

        public static int SolveLine(string line)
        {
            var model = new Model();
            var tokens = line
                .Tokenize()
                .Where(s => !string.Equals(s, "|"))
                .Select(s => new string(s.ToCharArray().OrderBy(c => c).ToArray()))
                .ToList();

            var uniqueTokens = tokens.Distinct().ToList();
            while (uniqueTokens.Any())
            {
                var tokenToLearn = uniqueTokens.First();
                uniqueTokens.RemoveAt(0);

                if (!model.TryLearn(tokenToLearn))
                {
                    uniqueTokens.Add(tokenToLearn);
                }
            }

            return tokens.Skip(10).Select((s, i) => (int)Math.Pow(10, 3 - i) * model.Lookup(s)).Sum();
        }
    }
}