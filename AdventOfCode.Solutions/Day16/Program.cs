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
            result = Solve(lines.Single(), Problem1);
            Console.WriteLine($"Score: {result}");
            Console.WriteLine();

            result = Solve(lines.Single(), Problem2);
            Console.WriteLine($"Score: {result}");
            Console.WriteLine();
        }

        public static long Solve(IEnumerable<char> input, Func<IEnumerable<IPacket>, long> evaluate)
        {
            var packets = input.ToBitStream().ToPacketStream().ToList();
            return evaluate(packets);
        }

        public static long Problem1(IEnumerable<IPacket> packets) => packets.Sum(p => Tally(p));

        public static long Problem2(IEnumerable<IPacket> packets) => packets.Single().Evaluate();

        private static int Tally(IPacket packet)
        {
            return packet.Version + packet.Subpackets.Sum(p => Tally(p));
        }
    }
}