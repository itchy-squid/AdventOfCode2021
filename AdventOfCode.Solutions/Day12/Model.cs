using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day12
{
    internal class Node
    {
        public string Name { get; init; }
        private List<Node> _nodes = new List<Node>();

        public void AddRelationship(Node node)
        {
            _nodes.Add(node);
        }

        public virtual IEnumerable<ImmutableList<string>> GetPaths(ImmutableList<string> pathStart)
        {
            var continuedPath = pathStart.Add(Name);

            var paths = _nodes
                .Where(node => node.CanVisit(continuedPath))
                .SelectMany(node => node.GetPaths(continuedPath))
                .ToList();

            return paths;
        }

        protected bool CanVisit(ImmutableList<string> path)
        {
            return !(Name[0] >= 'a' && Name[0] <= 'z') || !path.Any(x => string.Equals(x, Name));
        }
    }

    internal class EndNode : Node
    {
        public override IEnumerable<ImmutableList<string>> GetPaths(ImmutableList<string> pathStart)
        {
            return new[] { pathStart.Add(Name) };
        }
    }
}
