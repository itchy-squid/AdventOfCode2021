using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day18
{

    public interface ISnailfishNumber
    {
        string ToString();

        int Magnitude();

        bool TryExplode();
        bool TryExplode(ImmutableStack<SnailfishOperator> parents, int side);

        bool TrySplit();
        bool TrySplit(ImmutableStack<SnailfishOperator> parents, int side);
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

        public bool TryExplode(ImmutableStack<SnailfishOperator> parents, int side)
        {
            return false;
        }

        public bool TrySplit()
        {
            return false;
        }

        public bool TrySplit(ImmutableStack<SnailfishOperator> parents, int side)
        {
            if (Value < 10) return false;

            var leftValue = Value / 2;
            var newNode = new SnailfishOperator()
            {
                Left = new SnailfishLiteral(leftValue),
                Right = new SnailfishLiteral(Value - leftValue)
            };

            if (side == -1) parents.First().Left = newNode;
            else if (side == 1) parents.First().Right = newNode;
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
            return TryExplode(ImmutableStack<SnailfishOperator>.Empty, 0);
        }

        public bool TryExplode(ImmutableStack<SnailfishOperator> parents, int side)
        {
            if (parents.Count() >= 4 && Left is SnailfishLiteral left && Right is SnailfishLiteral right)
            {
                var leftTarget = parents.FirstOrDefault(p => p.Left is SnailfishLiteral);
                var rightTarget = parents.FirstOrDefault(p => p.Right is SnailfishLiteral);

                if (leftTarget?.Left is SnailfishLiteral leftLiteral) leftTarget.Left = leftLiteral + left;
                if (rightTarget?.Right is SnailfishLiteral rightLiteral) rightTarget.Right = rightLiteral + right;

                if (side == -1) parents.First().Left = new SnailfishLiteral(0);
                else if (side == 1) parents.First().Right = new SnailfishLiteral(0);
                else throw new ApplicationException();

                return true;
            }

            return Left!.TryExplode(parents.Push(this), -1)
                || Right!.TryExplode(parents.Push(this), 1);
        }

        public bool TrySplit()
        {
            return TrySplit(ImmutableStack<SnailfishOperator>.Empty, 0);
        }

        public bool TrySplit(ImmutableStack<SnailfishOperator> parents, int side)
        {
            return Left!.TrySplit(parents.Push(this), -1)
                || Right!.TrySplit(parents.Push(this), 1);
        }
    }
}
