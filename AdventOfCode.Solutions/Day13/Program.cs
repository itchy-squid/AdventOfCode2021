using AdventOfCode.Solutions.Tools;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Day13
{
    public static class Program
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day13");

            var result = Solve(input);
            Console.WriteLine(result);
            Console.WriteLine();
        }

        public static int Solve(IEnumerable<string> lines)
        {
            var reader = lines.GetEnumerator();

            var startingModel = new Paper(GetPoints(reader));
            var endingModel = ApplyInstructions(reader, startingModel, 1);

            return endingModel.Points.Count;
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

        private static Paper ApplyInstructions(IEnumerator<string> lineReader, Paper model, int steps)
        {
            var regex = new Regex(@"fold along ((?<foldX>x)|y)=(?<position>\d+)");
            for (int i = 0; i < steps && lineReader.MoveNext(); i++)
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
                X = pt.X <= x ? x : x - (pt.X - x),
                Y = pt.Y
            }));
        }

        public Paper FoldY(int y)
        {
            return new Paper(Points.Select(pt => new Point
            {
                X = pt.X,
                Y = pt.Y <= y ? y : y - (pt.Y - y)
            }));
        }
    }
}
