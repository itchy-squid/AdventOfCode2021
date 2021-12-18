using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day16
{
    public class CountOperatorPacketParser : IPacketParser
    {
        bool IPacketParser.IsParser(int version, int type, IEnumerable<int> content)
        {
            var bit = content.First();
            return type != LiteralPacket.Type && bit == 1;
        }

        (IPacket, IEnumerable<int>) IPacketParser.ParseNext(int version, int type, IEnumerable<int> content)
        {
            (var _, content) = (content.Take(1).ToList(), content.Skip(1).ToList());
            (var nSubpackets, content) = (content.Take(11).ToInt(), content.Skip(11).ToList());

            List<IPacket> packets = new();
            for (var i = 0; i < nSubpackets; i++)
            {
                (var packet, content) = content.ParseNext();
                if (packet == null) throw new ApplicationException();
                packets.Add(packet);
            }

            return (new CountOperatorPacket(version, packets), content);
        }
    }

    public class CountOperatorPacket : IPacket
    {
        public int Version { get; }

        public ImmutableList<IPacket> Subpackets { get;  }

        IEnumerable<IPacket> IPacket.Subpackets => Subpackets;

        public CountOperatorPacket(int version, IEnumerable<IPacket> subpackets)
        {
            Version = version;
            Subpackets = subpackets.ToImmutableList();
        }
    }
}
