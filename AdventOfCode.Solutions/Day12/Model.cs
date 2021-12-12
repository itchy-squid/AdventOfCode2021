using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day12
{
    public class Node
    {
        public string Name { get; private init; }
        public Func<ImmutableList<string>, bool> CanVisit { get; private init; }
        private List<Node> _nodes = new();

        public Node(string name, Func<ImmutableList<string>, bool> canVisit)
        {
            Name = name;
            CanVisit = canVisit;
        }

        internal void AddRelationship(Node node)
        {
            _nodes.Add(node);
        }

        internal virtual IEnumerable<ImmutableList<string>> GetPaths(ImmutableList<string> pathStart)
        {
            var continuedPath = pathStart.Add(Name);

            var paths = _nodes
                .Where(node => node.CanVisit(continuedPath))
                .SelectMany(node => node.GetPaths(continuedPath))
                .ToList();

            return paths;
        }
    }

    public class EndNode : Node
    {
        public EndNode(string name, Func<ImmutableList<string>, bool> canVisit) : base(name, canVisit)
        {
        }

        internal override IEnumerable<ImmutableList<string>> GetPaths(ImmutableList<string> pathStart)
        {
            return new[] { pathStart.Add(Name) };
        }
    }
}
