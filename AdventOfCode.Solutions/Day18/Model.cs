using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day18
{

    public interface ISnailfishNumber
    {
        string ToString();

        int Magnitude();

        bool TryExplode();
        bool TryExplode(ImmutableStack<(SnailfishOperator, int)> history);

        bool TrySplit();
        bool TrySplit(ImmutableStack<(SnailfishOperator, int)> history);
    }

    public class SnailfishLiteral : ISnailfishNumber
    {
        public int Value { get; init; }

        public SnailfishLiteral(int value)
        {
            Value = value;
        }

        public static SnailfishLiteral operator +(SnailfishLiteral a, SnailfishLiteral b)
        {
            return new SnailfishLiteral(a.Value + b.Value);
        }

        public int Magnitude() => Value;

        public override string ToString()
        {
            return Value.ToString();
        }

        public bool TryExplode()
        {
            return false;
        }

        public bool TryExplode(ImmutableStack<(SnailfishOperator, int)> history)
        {
            return false;
        }

        public bool TrySplit()
        {
            return false;
        }

        public bool TrySplit(ImmutableStack<(SnailfishOperator, int)> history)
        {
            if (Value < 10) return false;

            var leftValue = Value / 2;
            var newNode = new SnailfishOperator()
            {
                Left = new SnailfishLiteral(leftValue),
                Right = new SnailfishLiteral(Value - leftValue)
            };

            var (parent, side) = history.First();

            if (side == -1) parent.Left = newNode;
            else if (side == 1) parent.Right = newNode;
            else throw new ApplicationException();

            return true;
        }
    }


    public class SnailfishOperator : ISnailfishNumber
    {
        public ISnailfishNumber this[int key]
        {
            get 
            {
                return key switch
                {
                    -1 => Left!,
                    1 => Right!,
                    _ => throw new ApplicationException()
                };
            }

            set
            {
                switch (key)
                {
                    case -1: Left = value; break;
                    case 1: Right = value; break;
                    default: throw new ApplicationException();
                };
            }
        }

        public ISnailfishNumber? Left { get; set; }
        public ISnailfishNumber? Right { get; set; }

        public override string ToString()
        {
            return $"[{Left!.ToString()},{Right!.ToString()}]";
        }

        public int Magnitude() => (3 * Left!.Magnitude()) + (2 * Right!.Magnitude());

        public bool TryExplode()
        {
            return TryExplode(ImmutableStack<(SnailfishOperator, int)>.Empty);
        }

        private static (SnailfishOperator?, int) FindLeftOrDefault(ImmutableStack<(SnailfishOperator, int)> history)
        {
            var (parent, side) = history.FirstOrDefault();
            if (parent == null) return (null, 0);

            if (side == -1) return FindLeftOrDefault(history.Pop());
            if (side != 1) throw new ApplicationException();

            if (parent.Left is SnailfishLiteral) return (parent, -1);
            else
            {
                var curr = parent.Left as SnailfishOperator;
                while (curr != null)
                {
                    if (curr.Right is SnailfishLiteral) return (curr, 1);
                    curr = curr.Right as SnailfishOperator;
                }
            }

            return (null, 0);
        }

        private static (SnailfishOperator?, int) FindRightOrDefault(ImmutableStack<(SnailfishOperator, int)> history)
        {
            var (parent, side) = history.FirstOrDefault();
            if (parent == null) return (null, 0);

            if (side == 1) return FindRightOrDefault(history.Pop());
            if (side != -1) throw new ApplicationException();

            if (parent.Right is SnailfishLiteral) return (parent, 1);
            else
            {
                var curr = parent.Right as SnailfishOperator;
                while (curr != null)
                {
                    if (curr.Left is SnailfishLiteral) return (curr, -1);
                    curr = curr.Left as SnailfishOperator;
                }
            }

            return (null, 0);
        }

        public bool TryExplode(ImmutableStack<(SnailfishOperator, int)> history)
        {
            if (history.Count() >= 4 && Left is SnailfishLiteral left && Right is SnailfishLiteral right)
            {
                var (leftTarget, leftSide) = FindLeftOrDefault(history);
                var (rightTarget, rightSide) = FindRightOrDefault(history);

                if (leftTarget?[leftSide] is SnailfishLiteral leftLiteral) leftTarget[leftSide] = leftLiteral + left;
                if (rightTarget?[rightSide] is SnailfishLiteral rightLiteral) rightTarget[rightSide] = rightLiteral + right;

                var (parent, side) = history.First();
                parent[side] = new SnailfishLiteral(0);

                return true;
            }

            return Left!.TryExplode(history.Push((this, -1)))
                || Right!.TryExplode(history.Push((this, 1)));
        }

        public bool TrySplit()
        {
            return TrySplit(ImmutableStack<(SnailfishOperator, int)>.Empty);
        }

        public bool TrySplit(ImmutableStack<(SnailfishOperator, int)> history)
        {
            return Left!.TrySplit(history.Push((this, -1)))
                || Right!.TrySplit(history.Push((this, 1)));
        }
    }
}
