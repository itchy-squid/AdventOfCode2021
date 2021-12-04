using AdventOfCode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    public class Program
    {
        public const string InputDirectory = "Day3";

        public static void Main()
        {
            var lines = Input.ReadAllLines(InputDirectory);
            var (gamma, epsilon, power) = Solve(lines);

            Console.WriteLine($"gamma: {Convert.ToString(gamma, 2)}");
            Console.WriteLine($"epsilon: {Convert.ToString(epsilon, 2)}");
            Console.WriteLine($"gamma x epsilon = {power}");
        }

        public static (int, int, int) Solve(IEnumerable<string> lines)
        {
            var charLines = lines.Select(line => line.ToCharArray()).ToArray();

            var charColumns = Enumerable
                .Range(0, charLines[0].Length)
                .Select(digit => charLines.Select(charLine => charLine[digit]));

            var bestChars = charColumns.Select(cs => cs.GroupBy(c => c).OrderBy(g => g.Count()).First().First());
            var bestBits = bestChars.Select(c => c == '1' ? 1 : 0).ToArray();

            int gamma = 0;
            for (int i = 0; i < bestBits.Count(); i++)
            {
                gamma <<= 1;
                gamma |= bestBits[i];
            }

            int ones = (int)Math.Pow(2, bestBits.Count()) - 1;
            int epsilon = ones ^ gamma;

            return (gamma, epsilon, gamma * epsilon);
        }
    }
}
