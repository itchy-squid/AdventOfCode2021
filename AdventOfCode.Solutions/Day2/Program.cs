using AdventOfCode.Solutions.Tools;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Day2
{
    public class Program
    {
        public const string InputFilename = "Input.txt";
        public const string InputDirectory = "Day2";

        public const string Pattern = @"(forward (?<forwardNo>\d*))|(down (?<downNo>\d*))|(up (?<upNo>\d*))|\n";

        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            var text = await Input.ReadAllTextAsync(InputDirectory);

            var sub1 = new Problem1Submarine();
            Solve(text, sub1);
            Dump(sub1);

            var sub2 = new Problem2Submarine();
            Solve(text, sub2);
            Dump(sub2);
        }

        public static void Solve(string input, Submarine submarine)
        {
            var regex = new Regex(Pattern);

            var tuples = regex
                .Matches(input)
                .Select(match => Instruction.CreateFrom(match));

            foreach (var tuple in tuples)
            {
                submarine.Move(tuple);
            }
        }

        public static void Dump(Submarine submarine)
        {
            Console.WriteLine($"Horizontal: {submarine.Horizontal}");
            Console.WriteLine($"Depth: {submarine.Depth}");
            Console.WriteLine($"Aim: {submarine.Aim}");
            Console.WriteLine($"Forward x Depth = {submarine.Horizontal * submarine.Depth}");
            Console.WriteLine();
        }
    }
}