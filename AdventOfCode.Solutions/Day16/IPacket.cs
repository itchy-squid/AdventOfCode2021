namespace AdventOfCode.Solutions.Day16
{
    public interface IPacketParser
    {
        bool IsParser(int version, int type, IEnumerable<int> content);

        (IPacket, IEnumerable<int>) ParseNext(int version, int type, IEnumerable<int> content);
    }

    public interface IPacket
    {
        int Version { get; }

        IEnumerable<IPacket> Subpackets { get; }

        long Evaluate();
    }
}
