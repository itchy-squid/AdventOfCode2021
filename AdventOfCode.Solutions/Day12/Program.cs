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
            result = Solve(input);
            Console.WriteLine($"No. of unique paths: {result}");
        }

        public static int Solve(IEnumerable<string> input)
        {
            var start = Parse(input);

            var paths = start
                .GetPaths(Enumerable.Empty<string>().ToImmutableList())
                .Select(path => path.Aggregate((a, b) => $"{a}-{b}"))
                .ToList();

            var dump = paths.Aggregate((one, two) => $"{one}\r\n{two}");

            return paths.Count();
        }

        private static Node Parse(IEnumerable<string> lines)
        {
            Regex regex = new Regex("(?<node1>[a-zA-Z]+)-(?<node2>[a-zA-Z]+)");
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();
            Func<string, Node> create = 
                name => name == "end" 
                ? new EndNode() { Name = "end" }
                : new Node() { Name = name };

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
    }
}