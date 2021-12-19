using AdventOfCode.Solutions.DataStructures;
using AdventOfCode.Solutions.Tools;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Day17
{
    public static class Program
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day17");

            var result1 = Solve(input.Single(), Problem1);
            Console.WriteLine(result1);
            Console.WriteLine();

            var result2 = Solve(input.Single(), Problem2);
            Console.WriteLine(result2);
            Console.WriteLine();
        }

        public static int Solve(string line, Func<IEnumerable<IEnumerable<Point>>, int> calculator)
        {
            var regex = new Regex(@"target area: x=(?<xMin>-?\d+)\.\.(?<xMax>-?\d+), y=(?<yMin>-?\d+)\.\.(?<yMax>-?\d+)");
            var match = regex.Match(line);

            var xMin = int.Parse(match.Groups["xMin"].Value);
            var xMax = int.Parse(match.Groups["xMax"].Value);
            var yMin = int.Parse(match.Groups["yMin"].Value);
            var yMax = int.Parse(match.Groups["yMax"].Value);

            var paths = Simulation.BruteForce((xMin, xMax), (yMin, yMax));
            var result = calculator(paths);

            return result;
        }

        public static int Problem1(IEnumerable<IEnumerable<Point>> paths)
        {
            var tallestPoint = paths
                .Select(path => path.MaxBy(pt => pt.Y)!.Y)
                .Max();

            return tallestPoint;
        }

        public static int Problem2(IEnumerable<IEnumerable<Point>> paths)
        {
            return paths.Count();
        }
    }

    public static class Simulation
    {
        public static IEnumerable<Point>? Run(Region region, Vector initialVelocity)
        {
            var position = new Point();
            var velocity = initialVelocity;
            List<Point> path = new();

            while (!position.Exceeds(region))
            {
                path.Add(position);
                position += velocity;
                velocity += (-1 * Math.Sign(velocity.X), -1);

                if (position.Intersects(region)) return path;
            }

            return null;
        }

        public static IEnumerable<IEnumerable<Point>> BruteForce(
            DataStructures.Range xRange, 
            DataStructures.Range yRange)
        {
            var region = new Region(xRange, yRange);

            var viablePaths = Enumerable.Range(1, region.RangeX.Max)
                .SelectMany(x => Enumerable.Range(region.RangeY.Min, region.RangeX.Max).Select(y => (x, y)))
                .Select(v => Run(region, v))
                .Where(path => path != null)
                .Select(path => path!)
                .ToList();

            return viablePaths;
        }
    }

    //public class Snapshot
    //{
    //    public Vector Velocity { get; init; }
    //    public Point Position { get; init; }

    //    public Snapshot()
    //    {
    //        Velocity = new Vector();
    //        Position = new Point();
    //    }
    //}
}
