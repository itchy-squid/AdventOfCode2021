using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Services
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
    }
}
