namespace AdventOfCode.Solutions.Day16
{
    public class LiteralPacketParser : IPacketParser
    {
        public bool IsParser(int version, int type, IEnumerable<int> content)
        {
            return type == LiteralPacket.Type;
        }

        public (IPacket, IEnumerable<int>) ParseNext(int version, int type, IEnumerable<int> content)
        {
            bool continueBit;
            do
            {
                (continueBit, content) = (content.Take(1).Single() == 1, content.Skip(1));
                (var chunk, content) = (content.Take(4).ToInt(), content.Skip(4).ToList());
            } while (continueBit);

            return (new LiteralPacket(version), content);
        }
    }

    public class LiteralPacket : IPacket
    {
        public const int Type = 4;

        public int Version { get; init; }

        public IEnumerable<IPacket> Subpackets => Enumerable.Empty<IPacket>();

        public LiteralPacket(int version)
        {
            Version = version;
        }
    }
}
