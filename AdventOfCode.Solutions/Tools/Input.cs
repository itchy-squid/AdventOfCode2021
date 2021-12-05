﻿namespace AdventOfCode.Solutions.Services
{
    internal class Input
    {
        public const string InputFilename = "Input.txt";

        public static IEnumerable<string> ReadAllLines(string directory)
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), directory, InputFilename);
            
            Console.WriteLine($"Reading input from {file}");
            return File.ReadAllLines(file);
        }

        public static Task<string> ReadAllTextAsync(string directory)
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), directory, InputFilename);

            Console.WriteLine($"Reading input from {file}");
            return File.ReadAllTextAsync(file);
        }

    }
}
