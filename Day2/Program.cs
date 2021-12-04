using System.Text.RegularExpressions;

namespace AdventOfCode.Day2
{
    public class Program
    {
        public const string InputFilename = "Input.txt";
        public const string InputDirectory = "Day2";

        public const string Pattern = @"(forward (?<forwardNo>\d*))|(down (?<downNo>\d*))|(up (?<upNo>\d*))|\n";

        public static async Task Main()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), InputDirectory, InputFilename);
            var regex = new Regex(Pattern);

            Console.WriteLine($"Reading input from {file}");
            var text = await File.ReadAllTextAsync(file);

            var matches = regex.Matches(text).ToList();

            var tuples = matches
                .Select(match => new
                {
                    Forward = ParseOrDefault(match.Groups["forwardNo"].Value),
                    Down = ParseOrDefault(match.Groups["downNo"].Value),
                    Up = ParseOrDefault(match.Groups["upNo"].Value)
                });

            int forward = 0;
            int depth = 0;
            int aim = 0;
            foreach (var tuple in tuples)
            {
                forward += tuple.Forward;
                depth += aim * tuple.Forward;
                aim += tuple.Down - tuple.Up;
            }

            Console.WriteLine($"Forward: {forward}");
            Console.WriteLine($"Depth: {depth}");
            Console.WriteLine($"Aim: {aim}");
            Console.WriteLine($"Forward x Depth = {forward * depth}");
        }

        public static int ParseOrDefault(string input)
        {
            return !string.IsNullOrEmpty(input) ? int.Parse(input) : 0;
        }
    }
}