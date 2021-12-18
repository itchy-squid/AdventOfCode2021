using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day16
{
    public static class BitStreamExtensions
    {
        private static readonly ImmutableList<IPacketParser> _packetParsers = new IPacketParser[]
            {
                new LiteralPacketParser(),
                new CountOperatorPacketParser(),
                new LengthOperatorPacketParser()
            }.ToImmutableList();

        public static (IPacket?, IEnumerable<int>) ParseNext(this IEnumerable<int> head)
        {
            var cursor = head;
            (var version, cursor) = (cursor.Take(3).ToInt(), cursor.Skip(3).ToList());
            if (!cursor.Any()) return (null, head);

            (var type, cursor) = (cursor.Take(3).ToInt(), cursor.Skip(3).ToList());
            if (!cursor.Any()) return (null, head);

            var parser = _packetParsers.FirstOrDefault(p => p.IsParser(version, type, cursor));
            if (parser != null)
            {
                (var packet, cursor) = parser.ParseNext(version, type, cursor);
                return (packet, cursor);
            }

            return (null, head);
        }

        public static IEnumerable<IPacket> ToPacketStream(this IEnumerable<int> bitStream)
        {
            var head = bitStream;
            while (head.Any())
            {
                (var packet, head) = head.ParseNext();
                if (packet != null) yield return packet;
                else break;
            }
        }

        public static IEnumerable<int> ToBitStream(this IEnumerable<char> hexStream)
        {
            return hexStream
                .Select(c => Convert.ToInt32($"{c}", 16))
                .SelectMany(b => new[] { (b & 0b1000) >> 3, (b & 0b100) >> 2, (b & 0b10) >> 1, (b & 0b1) });
        }

        public static int ToInt(this IEnumerable<int> bitStream)
        {
            var bits = bitStream.ToList();
            var nBits = bits.Count;
            return bits.Select((b, i) => b << (nBits - i - 1)).Aggregate(0, (a, b) => a | b);
        }

        public static long ToLong(this IEnumerable<int> bitStream)
        {
            var bits = bitStream.ToList();
            var nBits = bits.Count;
            return bits.Select((b, i) => (long)b << (nBits - i - 1)).Aggregate((long)0, (a, b) => a | b);
        }
    }
}
