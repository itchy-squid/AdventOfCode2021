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

        public bool TryExplode(ImmutableStack<(SnailfishOperator, int)> history)
        {
            if (history.Count() >= 4 && Left is SnailfishLiteral left && Right is SnailfishLiteral right)
            {
                var leftTarget = history.FirstOrDefault(p => p.Item1.Left is SnailfishLiteral);
                var rightTarget = history.FirstOrDefault(p => p.Item1.Right is SnailfishLiteral);

                if (leftTarget.Item1?.Left is SnailfishLiteral leftLiteral) leftTarget.Item1.Left = leftLiteral + left;
                if (rightTarget.Item1?.Right is SnailfishLiteral rightLiteral) rightTarget.Item1.Right = rightLiteral + right;

                var (parent, side) = history.First();

                if (side == -1) parent.Left = new SnailfishLiteral(0);
                else if (side == 1) parent.Right = new SnailfishLiteral(0);
                else throw new ApplicationException();

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
