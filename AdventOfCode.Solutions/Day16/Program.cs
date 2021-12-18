using AdventOfCode.Solutions.Tools;
using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day16
{
    public static class Program
    {
        public static void Main()
        {
            var lines = Input.ReadAllLines("Day16");

            long result;
            result = Solve(lines.Single());
            Console.WriteLine($"Score: {result}");
            Console.WriteLine();

            //result = Solve(lines);
            //Console.WriteLine($"Score: {result}");
            //Console.WriteLine();
        }

        public static long Solve(IEnumerable<char> input)
        {
            var packets = input.ToBitStream().ToPacketStream().ToList();
            return packets.Select(p => p.Version).Sum();
        }
    }
}