using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day12
{
    internal class Node
    {
        public string Name { get; private init; }
        public Func<ImmutableList<string>, bool> CanVisit { get; private init; }
        private List<Node> _nodes = new List<Node>();

        protected Node(string name, Func<ImmutableList<string>, bool> canVisit)
        {
            Name = name;
            CanVisit = canVisit;
        }

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

        public static Func<string, Node> Create(int maxVisits)
        {
            return (string name) =>
            {
                switch (name)
                {
                    case "start":
                        return new Node(name, (_) => false);

                    case "end":
                        return new EndNode(name, (_) => true);

                    case string s when 'a' <= s[0] && 'z' >= s[0]:
                        return new Node(name, path => path.Where(n => n == name).Count() < maxVisits);

                    default:
                        return new Node(name, _ => true);
                }
            };
        }
    }

    internal class EndNode : Node
    {
        public EndNode(string name, Func<ImmutableList<string>, bool> canVisit) : base(name, canVisit)
        {
        }

        public override IEnumerable<ImmutableList<string>> GetPaths(ImmutableList<string> pathStart)
        {
            return new[] { pathStart.Add(Name) };
        }
    }
}
