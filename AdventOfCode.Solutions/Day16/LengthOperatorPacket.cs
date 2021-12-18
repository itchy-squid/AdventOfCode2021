using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day16
{
    public class LengthOperatorPacketParser : IPacketParser
    {
        bool IPacketParser.IsParser(int version, int type, IEnumerable<int> content)
        {
            var bit = content.First();
            return type != LiteralPacket.Type && content.First() == 0;
        }

        (IPacket, IEnumerable<int>) IPacketParser.ParseNext(int version, int type, IEnumerable<int> content)
        {
            (var _, content)                 = (content.Take(1), content.Skip(1));
            (var subpacketsLength, content)  = (content.Take(15).ToInt(), content.Skip(15));
            (var subpacketsContent, content) = (content.Take(subpacketsLength).ToList(), content.Skip(subpacketsLength).ToList());

            return (new LengthOperatorPacket(version, subpacketsContent.ToPacketStream()), content);
        }
    }

    public class LengthOperatorPacket : IPacket
    {
        public int Version { get; }

        public ImmutableList<IPacket> Subpackets { get; }

        IEnumerable<IPacket> IPacket.Subpackets => Subpackets;

        public LengthOperatorPacket(int version, IEnumerable<IPacket> subpackets)
        {
            Version = version;
            Subpackets = subpackets.ToImmutableList();
        }
    }
}
