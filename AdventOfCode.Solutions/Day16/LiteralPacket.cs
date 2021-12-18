namespace AdventOfCode.Solutions.Day16
{
    public class LiteralPacket : IPacket
    {
        public int Version { get; }

        public long Value { get; }

        public PacketTypes Type { get; }

        IEnumerable<IPacket> IPacket.Subpackets => Enumerable.Empty<IPacket>();

        public LiteralPacket(int version, long value)
        {
            Version = version;
            Value = value;
        }

        public long Evaluate()
        {
            return Value;
        }
    }
}
