using AdventOfCode.Solutions.Tools;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Day18
{
    public class Program
    {
        public static void Main()
        {
            var lines = Input.ReadAllLines("Day18");

            var result1 = Solve(lines);
            Console.WriteLine(result1);
            Console.WriteLine();
        }

        public static int Solve(IEnumerable<string> lines)
        {
            return Parser.Parse(lines).Magnitude();
        }
    }
}
