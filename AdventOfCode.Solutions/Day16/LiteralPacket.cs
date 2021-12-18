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
            while(content.First() != 0)
            {
                content = content.Skip(5);
            }

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
