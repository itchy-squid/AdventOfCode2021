using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day8
{
    public class Program2
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day8");

            var count = Solve(input);
            Console.WriteLine($"Count: {count}");
            Console.WriteLine();
        }

        public static int Solve(IEnumerable<string> input)
        {
            return input
                .Select(line => Solve(line))
                .Sum();
        }

        public static int Solve(string line)
        {
            var model = new Model();
            var tokens = line
                .SplitAndClean()
                .Where(s => !string.Equals(s, "|"))
                .Select(s => new string(s.ToCharArray().OrderBy(c => c).ToArray()))
                .ToList();

            var uniqueTokens = tokens.Distinct().ToList();

            while (uniqueTokens.Any())
            {
                var tokenToLearn = uniqueTokens.First();
                uniqueTokens.RemoveAt(0);

                if(!model.TryLearn(tokenToLearn))
                {
                    uniqueTokens.Add(tokenToLearn);
                }
            }

            return tokens.Skip(10).Select((s, i) => (int) Math.Pow(10, 3 - i) * model.Lookup(s)).Sum();
        }
    }
}