namespace AdventOfCode.Solutions.DataStructures
{
    public struct Point
    {
        public int X { get; init; }
        public int Y { get; init; }

        public static implicit operator Point((int, int) point) => new Point() { X = point.Item1, Y = point.Item2 };
        public static implicit operator(int, int)(Point point) => (point.X, point.Y);

        public static Point operator +(Point point, Vector vector) => (point.X + vector.X, point.Y + vector.Y);
    }

    public class Vector
    {
        public int X { get; init; }
        public int Y { get; init; }

        public static implicit operator Vector((int, int) vector) => new Vector() { X = vector.Item1, Y = vector.Item2 };
        public static implicit operator (int, int)(Vector vector) => (vector.X, vector.Y);

        public static Vector operator +(Vector a, Vector b) => (a.X + b.X, a.Y + b.Y);
    }

    public class Range
    {
        public int Min { get; init; }
        public int Max { get; init; }

        public static implicit operator Range((int, int) range) => new Range() { Min = range.Item1, Max = range.Item2 };
        public static implicit operator (int, int)(Range range) => (range.Min, range.Max);

        public bool Includes(int x)
        {
            return x >= Min && x <= Max;
        }
    }

    public class Region
    {
        public Range RangeX { get; init; }
        public Range RangeY { get; init; }

        public Region(Range rangeX, Range rangeY)
        {
            RangeX = rangeX;
            RangeY = rangeY;
        }
    }

    public static class RegionExtensions
    {
        public static bool Intersects(this Point pt, Region region)
        {
            return region.RangeX.Includes(pt.X) && region.RangeY.Includes(pt.Y);
        }

        public static bool Exceeds(this Point pt, Region region)
        {
            return pt.X > region.RangeX.Max || pt.Y < region.RangeY.Min;
        }
    }
}
