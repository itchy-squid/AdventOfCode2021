using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day3
{
    public class Program1
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
            var nDigits = charLines[0].Length;

            var charColumns = Enumerable
                .Range(0, nDigits)
                .Select(digit => charLines.Select(charLine => charLine[digit]));

            var bestChars = charColumns.Select(cs => cs.GroupBy(c => c).OrderBy(g => g.Count()).First().First()).ToArray();

            int gamma = Convert.ToInt32(new string(bestChars), 2);
            int ones = (int)Math.Pow(2, nDigits) - 1;
            int epsilon = ones ^ gamma;

            return (gamma, epsilon, gamma * epsilon);
        }
    }
}
