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

        public override string ToString() => Value.ToString();

        public bool TryExplode() => false;

        public bool TryExplode(ImmutableStack<(SnailfishOperator, int)> history) => false;

        public bool TrySplit() => false;

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

        public override string ToString() => $"[{Left!.ToString()},{Right!.ToString()}]";

        public int Magnitude() => (3 * Left!.Magnitude()) + (2 * Right!.Magnitude());

        public static SnailfishOperator operator +(SnailfishOperator a, ISnailfishNumber b)
        {
            var result = new SnailfishOperator()
            {
                Left = a,
                Right = b
            };

            while (result.TryExplode() || result.TrySplit()) ;

            return result;
        }

        public bool TryExplode()
        {
            return TryExplode(ImmutableStack<(SnailfishOperator, int)>.Empty);
        }

        public bool TryExplode(ImmutableStack<(SnailfishOperator, int)> history)
        {
            if (history.Count() >= 4 && Left is SnailfishLiteral left && Right is SnailfishLiteral right)
            {
                var (leftTarget, leftSide) = FindClosestOrDefault(history, -1);
                var (rightTarget, rightSide) = FindClosestOrDefault(history, 1);

                if (leftTarget?[leftSide] is SnailfishLiteral leftLiteral) leftTarget[leftSide] = leftLiteral + left;
                if (rightTarget?[rightSide] is SnailfishLiteral rightLiteral) rightTarget[rightSide] = rightLiteral + right;

                var (parent, side) = history.First();
                parent[side] = new SnailfishLiteral(0);

                return true;
            }

            return Left!.TryExplode(history.Push((this, -1)))
                || Right!.TryExplode(history.Push((this, 1)));
        }

        private static (SnailfishOperator?, int) FindClosestOrDefault(ImmutableStack<(SnailfishOperator, int)> history, int sign)
        {
            var (parent, side) = history.FirstOrDefault();
            if (parent == null) return (null, 0);

            if (side == sign) return FindClosestOrDefault(history.Pop(), sign);
            if (side != -1 * sign) throw new ApplicationException();

            if (parent[sign] is SnailfishLiteral) return (parent, sign);
            else
            {
                var curr = parent[sign] as SnailfishOperator;
                while (curr != null)
                {
                    if (curr[-1 * sign] is SnailfishLiteral) return (curr, -1 * sign);
                    curr = curr[-1 * sign] as SnailfishOperator;
                }
            }

            return (null, 0);
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
