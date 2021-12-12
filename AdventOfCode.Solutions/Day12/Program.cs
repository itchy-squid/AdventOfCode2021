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
            result = Solve(input, 1);
            Console.WriteLine($"No. of unique paths: {result}");
            Console.WriteLine();

            result = Solve(input, 2);
            Console.WriteLine($"No. of unique paths: {result}");
            Console.WriteLine();
        }

        public static int Solve(IEnumerable<string> input, int maxSmallNodeVisits)
        {
            var start = Parse(input, maxSmallNodeVisits);

            var paths = start
                .GetPaths(Enumerable.Empty<string>().ToImmutableList())
                .Select(path => path.Aggregate((a, b) => $"{a}-{b}"))
                .ToList();

            return paths.Count();
        }

        private static Node Parse(IEnumerable<string> lines, int maxSmallNodeVisits)
        {
            Regex regex = new Regex("(?<node1>[a-zA-Z]+)-(?<node2>[a-zA-Z]+)");
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            foreach(var line in lines)
            {
                var match = regex.Match(line);
                var node1 = nodes.FetchOrCreate(match.Groups["node1"].Value, Node.Create(maxSmallNodeVisits));
                var node2 = nodes.FetchOrCreate(match.Groups["node2"].Value, Node.Create(maxSmallNodeVisits));

                node1.AddRelationship(node2);
                node2.AddRelationship(node1);
            }

            return nodes["start"];
        }
    }
}