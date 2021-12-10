using AdventOfCode.Solutions.Tools;
using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day10
{
    public static class Program
    {
        public static void Main()
        {
            var lines = Input.ReadAllLines("Day10");

            long result;
            result = Solve(lines, new Problem1ScoreBuilder());
            Console.WriteLine($"Score: {result}");
            Console.WriteLine();

            result = Solve(lines, new Problem2ScoreBuilder());
            Console.WriteLine($"Score: {result}");
            Console.WriteLine();
        }

        public static long Solve(IEnumerable<string> lines, IScoreBuilder builder)
        {
            lines.Select(line => SolveLine(line))
                .ForEach(result => builder.AddResult(result!.Item1, result.Item2));

            return builder.ToScore();
        }

        public static (char?, IEnumerable<char>?) SolveLine(string line)
        {
            Stack<char> unresolvedOpeningCharacters = new();

            foreach (char c in line)
            {
                if (c.IsOpeningCharacter())
                {
                    unresolvedOpeningCharacters.Push(c);
                    continue;
                }

                char? curr = unresolvedOpeningCharacters.Any() ? unresolvedOpeningCharacters.Peek() : null;
                if(!curr.HasValue || !c.MatchesOpeningCharacters(curr.Value))
                {
                    return (c, null);
                }

                unresolvedOpeningCharacters.Pop();
            }

            return (null, unresolvedOpeningCharacters);
        }
    }
}