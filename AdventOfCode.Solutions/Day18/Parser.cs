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

        public static ISnailfishNumber Parse(string line)
        {
            var stack = new Stack<(SnailfishOperator, int)>();
            ISnailfishNumber? head = null;

            foreach (char c in line)
            {
                switch (c)
                {
                    case '[':
                        {
                            var curr = stack.FirstOrDefault();
                            var child = new SnailfishOperator();

                            AssignChild(curr, child);

                            stack.Push((child, -1));
                            break;
                        }

                    case char _ when c >= '0' && c <= '9':
                        {
                            var curr = stack.FirstOrDefault();
                            var child = new SnailfishLiteral(c.ToInt());

                            AssignChild(curr, child);
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

        private static void AssignChild((SnailfishOperator, int) curr, ISnailfishNumber child)
        {
            var (parent, parentSide) = curr;

            if (parent != null)
            {
                if (parentSide == -1) parent.Left = child;
                else if (parentSide == 1) parent.Right = child;
                else throw new ApplicationException();
            }
        }
    }
}
