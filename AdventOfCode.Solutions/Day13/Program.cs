using AdventOfCode.Solutions.Tools;
using System.Collections.Immutable;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Day13
{
    public static class Program
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day13");

            var result1 = Problem1Solve(input);
            Console.WriteLine(result1);
            Console.WriteLine();

            var result2 = Problem2Solve(input);
            Console.WriteLine(result2);
            Console.WriteLine();
        }

        public static int Problem1Solve(IEnumerable<string> lines, int? steps = 1)
        {
            var reader = lines.GetEnumerator();

            var startingModel = new Paper(GetPoints(reader));
            var endingModel = ApplyInstructions(reader, startingModel, steps);

            return endingModel.Points.Distinct().Count();
        }

        public static string Problem2Solve(IEnumerable<string> lines)
        {
            var reader = lines.GetEnumerator();

            var startingModel = new Paper(GetPoints(reader));
            var endingModel = ApplyInstructions(reader, startingModel, null);

            return endingModel.ToString();
        }

        private static IEnumerable<Point> GetPoints(IEnumerator<string> lineReader)
        {
            var regex = new Regex(@"(?<x>\d+),(?<y>\d+)");
            while (lineReader.MoveNext() && !string.IsNullOrWhiteSpace(lineReader.Current))
            {
                var match = regex.Match(lineReader.Current);
                yield return new Point { 
                    X = int.Parse(match.Groups["x"].Value), 
                    Y = int.Parse(match.Groups["y"].Value)
                };
            }
        }

        private static Paper ApplyInstructions(IEnumerator<string> lineReader, Paper model, int? steps)
        {
            var regex = new Regex(@"fold along ((?<foldX>x)|y)=(?<position>\d+)");
            for (int i = 0; (steps == null || i < steps) && lineReader.MoveNext(); i++)
            {
                var match = regex.Match(lineReader.Current);
                var position = int.Parse(match.Groups["position"].Value);
                model = match.Groups["foldX"].Success ? model.FoldX(position) : model.FoldY(position);
            }

            return model;
        }
    }

    public struct Point
    {
        public int X { get; init; }
        public int Y { get; init; }
    }

    public class Paper
    {
        public ImmutableList<Point> Points { get; private set; }

        public Paper(IEnumerable<Point> points)
        {
            Points = points.Distinct().ToImmutableList();
        }

        public Paper FoldX(int x)
        {
            return new Paper(Points.Select(pt => new Point
            {
                X = pt.X <= x ? pt.X : x - (pt.X - x),
                Y = pt.Y
            }));
        }

        public Paper FoldY(int y)
        {
            return new Paper(Points.Select(pt => new Point
            {
                X = pt.X,
                Y = pt.Y <= y ? pt.Y : y - (pt.Y - y)
            }));
        }

        public override string ToString()
        {
            var minX = Points.Select(pt => pt.X).Min();
            var maxX = Points.Select(pt => pt.X).Max();
            var minY = Points.Select(pt => pt.Y).Min();
            var maxY = Points.Select(pt => pt.Y).Max();

            var builder = new StringBuilder();
            var pointsByY = Points.GroupBy(pt => pt.Y);

            char[][] dump = Enumerable.Range(minY, maxY - minY + 1)
                .Select(y => Enumerable.Repeat('.', maxX - minX + 1).ToArray())
                .ToArray();

            Points.ForEach(pt => dump[pt.Y - minY][pt.X - minX] = '#');

            return dump.Select(line => new String(line)).Aggregate((a, b) => $"{a}{System.Environment.NewLine}{b}");
        }
    }
}
