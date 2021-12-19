﻿using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day18
{
    public class Program
    {
        public static void Main()
        {
            var lines = Input.ReadAllLines("Day18");

            var result1 = Problem1Solve(lines);
            Console.WriteLine(result1);
            Console.WriteLine();

            var result2 = Problem2Solve(lines);
            Console.WriteLine(result2);
            Console.WriteLine();
        }

        public static int Problem1Solve(IEnumerable<string> lines)
        {
            var models = lines.Select(line => Parser.Parse(line));
            var result = models.Aggregate((a, b) => a + b);
            return result.Magnitude();
        }

        public static int Problem2Solve(IEnumerable<string> lines)
        {
            var result = lines.Select(aLine => Parser.Parse(aLine))
                .SelectMany((a, i) =>
                {
                    return lines.Where((bLine, j) => j != i)
                        .Select(bLine => Parser.Parse(bLine))
                        .Select(b => a + b)
                        .Select(s => (s, s.Magnitude()));
                })
                .MaxBy(tuple => tuple.Item2);

            return result.Item2;
        }
    }
}
