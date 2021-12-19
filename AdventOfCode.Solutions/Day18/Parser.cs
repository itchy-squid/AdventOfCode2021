using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day18
{
    public class Parser
    {
        public static ISnailfishNumber Parse(IEnumerable<string> lines)
        {
            ISnailfishNumber? curr = null;

            foreach (var line in lines)
            {
                var next = Parse(line);

                if (curr == null) curr = next;
                else
                {
                    curr = new SnailfishOperator()
                    {
                        Left = curr,
                        Right = next
                    };
                }

                while (curr.TryExplode() || curr.TrySplit()) ;
            }

            return curr!;
        }

        public static SnailfishOperator Parse(string line)
        {
            var stack = new Stack<(SnailfishOperator, int)>();
            SnailfishOperator? head = null;

            foreach (char c in line)
            {
                switch (c)
                {
                    case '[':
                        {
                            var child = new SnailfishOperator();

                            var (parent, parentSide) = stack.FirstOrDefault();
                            if (parent != null)
                            {
                                parent[parentSide] = child;
                            }
                            
                            stack.Push((child, -1));
                            break;
                        }

                    case char _ when c >= '0' && c <= '9':
                        {
                            var child = new SnailfishLiteral(c.ToInt());

                            var (parent, parentSide) = stack.FirstOrDefault();
                            if (parent != null)
                            {
                                parent[parentSide] = child;
                            }

                            break;
                        }

                    case ',':
                        {
                            var (parent, _) = stack.Pop();
                            stack.Push((parent, 1));
                            break;
                        }

                    case ']':
                        {
                            (head, _) = stack.Pop();
                            break;
                        }
                }
            }

            return head!;
        }
    }
}
