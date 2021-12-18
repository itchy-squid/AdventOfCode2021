using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day16
{
    public class LiteralPacketParser : IPacketParser
    {
        public bool IsParser(int version, int type, IEnumerable<int> content)
        {
            return (PacketTypes)type == PacketTypes.CONST;
        }

        public (IPacket, IEnumerable<int>) ParseNext(int version, int type, IEnumerable<int> content)
        {
            bool continueBit;
            do
            {
                (continueBit, content) = (content.Take(1).Single() == 1, content.Skip(1));
                (var chunk, content) = (content.Take(4).ToInt(), content.Skip(4).ToList());
            } while (continueBit);

            return (new Packet(version, Enumerable.Empty<IPacket>()), content);
        }
    }

    public class LengthOperatorPacketParser : IPacketParser
    {
        bool IPacketParser.IsParser(int version, int type, IEnumerable<int> content)
        {
            var bit = content.First();
            return (PacketTypes)type != PacketTypes.CONST && content.First() == 0;
        }

        (IPacket, IEnumerable<int>) IPacketParser.ParseNext(int version, int type, IEnumerable<int> content)
        {
            (var _, content)                 = (content.Take(1), content.Skip(1));
            (var subpacketsLength, content)  = (content.Take(15).ToInt(), content.Skip(15));
            (var subpacketsContent, content) = (content.Take(subpacketsLength).ToList(), content.Skip(subpacketsLength).ToList());

            return (new Packet(version, subpacketsContent.ToPacketStream()), content);
        }
    }
    public class CountOperatorPacketParser : IPacketParser
    {
        bool IPacketParser.IsParser(int version, int type, IEnumerable<int> content)
        {
            var bit = content.First();
            return (PacketTypes)type != PacketTypes.CONST && bit == 1;
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

            return (new Packet(version, packets), content);
        }
    }
}
