using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day14
{
    public static class Program
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day14");

            var result1 = Solve(input, 10);
            Console.WriteLine(result1);
            Console.WriteLine();

            var result2 = Solve(input, 40);
            Console.WriteLine(result2);
            Console.WriteLine();
        }

        public static long Solve(IEnumerable<string> lines, int steps)
        {
            var polymer = Template(lines.First());
            var rules = Rules(lines.Skip(2));

            foreach(var pattern in polymer)
            {
                if (!rules.TryGetValue(pattern, out var next))
                {
                    next = new Rule(pattern);
                    rules.Add(pattern, next);
                }

                next.Apply(1);
            }
            rules.Values.ForEach(r => r.CompleteStep());

            for(var i = 0; i < steps; i++)
            {
                rules.Values.ForEach(r => r.BeginStep(rules));
                rules.Values.ForEach(r => r.CompleteStep());
            }

            var charFrequencies = rules
                .SelectMany(r => new[]
                    {
                        new { Character = r.Key[0], Count = r.Value.Count },
                        new { Character = r.Key[1], Count = r.Value.Count }
                    })
                .Concat(new[]
                {
                    new{Character = lines.First()[0], Count = (long)1},
                    new{Character = lines.First().Last(), Count = (long)1 }
                })
                .GroupBy(r => r.Character)
                .Select(g => new { Character = g.Key, Count = g.Select(kvp => kvp.Count).Sum() })
                .OrderBy(kvp => kvp.Count)
                .ToArray();

            return (charFrequencies[charFrequencies.Length - 1].Count - charFrequencies[0].Count) / (long)2;
        }

        private static IEnumerable<string> Template(string line)
        {
            return Enumerable
                .Range(0, line.Length - 1)
                .Select(i => line.Substring(i, 2));
        }

        private static Dictionary<string, Rule> Rules(IEnumerable<string> lines)
        {
            return lines
                .Select(rule => new Rule(rule.Substring(0, 2), rule[6]))
                .ToDictionary(rule => rule.Pattern);
        }

    }

    public class Rule
    {
        public string Pattern { get; init; }

        public string[] PatternsCreated { get; set; }

        public long Count { get; private set; }

        private long NextCount { get; set; }

        public Rule(string pattern, char? c = null)
        {
            Pattern = pattern;
            NextCount = 0;

            PatternsCreated = c == null
                ? new[] { pattern }
                : new string[] {
                    new string(new char[] { Pattern[0], c.Value }),
                    new string(new char[] { c.Value, Pattern[1]})
                };
        }

        public void BeginStep(Dictionary<string, Rule> rules)
        {
            PatternsCreated.ForEach(p => UpdateRule(rules, p));
        }

        private void UpdateRule(Dictionary<string, Rule> rules, string pattern)
        {
            if (!rules.TryGetValue(pattern, out var next))
            {
                next = new Rule(pattern);
                next.Count = 1;
                rules.Add(pattern, next);
            }

            next.Apply(Count);
        }

        public void Apply(long tally)
        {
            NextCount += tally;
        }

        public void CompleteStep()
        {
            Count = NextCount;
            NextCount = 0;
        }
    }
}
