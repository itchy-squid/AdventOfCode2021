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
            (var _, content)                = (content.Take(1), content.Skip(1));
            (var subPacketsLength, content) = (content.Take(15).ToInt(), content.Skip(15));
            (var _, content)                = (content.Take(subPacketsLength), content.Skip(subPacketsLength));
            return (new LengthOperatorPacket(version), content);
        }
    }

    public class LengthOperatorPacket : IPacket
    {
        public int Version { get; }

        public LengthOperatorPacket(int version)
        {
            Version = version;
        }
    }
}
