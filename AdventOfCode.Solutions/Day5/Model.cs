using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Day5
{
    internal class Point
    {
        public int X { get; init; }
        public int Y { get; init; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    internal class Line
    {
        private static Regex _regex = new Regex(@"(?<x1>\d+),(?<y1>\d+) -> (?<x2>\d+),(?<y2>\d+)");

        public Point Start { get; init; }
        public Point End { get; init; }

        public bool IsHorizontal => Start.Y == End.Y;

        public bool IsVertical => Start.X == End.X;

        private Line(int x1, int y1, int x2, int y2)
        {
            Start = new Point (x1, y1);
            End = new Point (x2, y2);
        }

        public bool Intersects(int x, int y)
        {
            return (x == Start.X && Start.Y <= y && y <= End.Y)
                || (y == Start.Y && Start.X <= x && x <= End.X);
        }

        public static Line? Parse(string input)
        {
            var match = _regex.Match(input);

            var x1 = int.Parse(match.Groups["x1"].Value);
            var x2 = int.Parse(match.Groups["x2"].Value);
            var y1 = int.Parse(match.Groups["y1"].Value);
            var y2 = int.Parse(match.Groups["y2"].Value);

            var flipped = x2 < x1 || y2 < y1;

            return flipped
                ? new Line(x2, y2, x1, y1)
                : new Line(x1, y1, x2, y2);
        }
    }

    internal class Field
    {
        public IEnumerable<Line> Lines { get; init; }

        public Point Start { get; init; }

        public Point End { get; init; }

        public int Width => End.X - Start.X + 1;

        public int Height => End.Y - Start.Y + 1;

        public Field(IEnumerable<Line> lines)
        {
            var minX = lines.Select(l => l.Start.X).Min();
            var minY = lines.Select(l => l.Start.Y).Min();
            var maxX = lines.Select(l => l.End.X).Max();
            var maxY = lines.Select(l => l.End.Y).Max();

            Start = new Point(minX, minY);
            End = new Point(maxX, maxY);
            Lines = lines.ToList();
        }

        public int Read(int x, int y)
        {
            return Lines.Where(l => l.Intersects(x, y)).Count();
        }
    }
}
