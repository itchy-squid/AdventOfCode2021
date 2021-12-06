using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Day5
{
    public struct Point
    {
        public int X { get; init; }
        public int Y { get; init; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Line
    {
        private static Regex _regex = new Regex(@"(?<x1>\d+),(?<y1>\d+) -> (?<x2>\d+),(?<y2>\d+)");

        public IEnumerable<Point> Points { get; init; }

        public bool IsHorizontal { get; init; }

        public bool IsVertical { get; init; }

        private Line(int x1, int y1, int x2, int y2)
        {
            var count = Math.Max(x2 - x1, Math.Abs(y2 - y1)) + 1;

            Points = RangeOrRepeat(x1, x2, count)
                .Zip(RangeOrRepeat(y1, y2, count))
                .Select(pt => new Point(pt.First, pt.Second))
                .ToList();

            IsHorizontal = y1 == y2;
            IsVertical = x1 == x2;
        }

        private IEnumerable<int> RangeOrRepeat(int start, int end, int count)
        {
            int sign = Math.Sign(end - start);
            return Enumerable.Range(0, count).Select(n => start + sign * n);
        }

        public static Line Parse(string input)
        {
            var match = _regex.Match(input);

            var x1 = int.Parse(match.Groups["x1"].Value);
            var x2 = int.Parse(match.Groups["x2"].Value);
            var y1 = int.Parse(match.Groups["y1"].Value);
            var y2 = int.Parse(match.Groups["y2"].Value);

            return x2 < x1
                ? new Line(x2, y2, x1, y1)
                : new Line(x1, y1, x2, y2);
        }
    }

    internal class Field
    {
        public IEnumerable<Line> Lines { get; init; }

        public Field(IEnumerable<Line> lines)
        {
            Lines = lines.ToList();
        }

        public IEnumerable<FieldPoint> Points()
        {
            return Lines
                .SelectMany(l => l.Points)
                .GroupBy(pt => pt)
                .Select(g => new FieldPoint {Point = g.Key, Count = g.Count() });
        }

        public class FieldPoint
        {
            public Point Point { get; init; }
            public int Count { get; init; }
        }
    }
}
