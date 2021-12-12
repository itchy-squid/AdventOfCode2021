using AdventOfCode.Solutions.Tools;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Day12
{
    public static class Program
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day12");

            int result;
            result = Solve(input, Problem1Create);
            Console.WriteLine($"No. of unique paths: {result}");
            Console.WriteLine();

            result = Solve(input, Problem2Create);
            Console.WriteLine($"No. of unique paths: {result}");
            Console.WriteLine();
        }

        public static int Solve(IEnumerable<string> input, Func<string, Node> create)
        {
            var start = Parse(input, create);

            var paths = start
                .GetPaths(Enumerable.Empty<string>().ToImmutableList())
                .Select(path => path.Aggregate((a, b) => $"{a}-{b}"))
                .ToList();

            return paths.Count();
        }

        private static Node Parse(IEnumerable<string> lines, Func<string, Node> create)
        {
            Regex regex = new Regex("(?<node1>[a-zA-Z]+)-(?<node2>[a-zA-Z]+)");
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            foreach(var line in lines)
            {
                var match = regex.Match(line);
                var node1 = nodes.FetchOrCreate(match.Groups["node1"].Value, create);
                var node2 = nodes.FetchOrCreate(match.Groups["node2"].Value, create);

                node1.AddRelationship(node2);
                node2.AddRelationship(node1);
            }

            return nodes["start"];
        }

        public static Node Problem1Create(string name)
        {
            return name switch
            {
                "start" => new Node(name, (_) => false),
                "end" => new EndNode(name, (_) => true),
                string s when char.IsLower(s[0]) => new Node(name, path => !path.Contains(name)),
                _ => new Node(name, _ => true),
            };
        }

        public static Node Problem2Create(string name)
        {
            Func<ImmutableList<string>, bool> canVisit =
                path =>
                {
                    var duplicate = path
                        .Where(n => char.IsLower(n[0]))
                        .GroupBy(n => n)
                        .Where(g => g.Count() > 1)
                        .Any();

                    return !duplicate || !path.Contains(name);
                };

            return name switch
            {
                "start" => new Node(name, (_) => false),
                "end" => new EndNode(name, (_) => true),
                string s when char.IsLower(s[0]) => new Node(name, canVisit),
                _ => new Node(name, _ => true),
            };
        }
    }
}