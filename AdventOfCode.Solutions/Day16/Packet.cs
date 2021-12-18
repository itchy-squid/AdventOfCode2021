using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Day16
{
    public enum PacketTypes : int
    {
        SUM = 0,
        PRODUCT = 1,
        MIN = 2,
        MAX = 3,
        CONST = 4,
        GT = 5,
        LT = 6,
        EQ = 7
    }

    public class Packet : IPacket
    {
        public int Version { get; }

        public ImmutableList<IPacket> Subpackets { get; }

        IEnumerable<IPacket> IPacket.Subpackets => Subpackets;

        public Packet(int version, IEnumerable<IPacket> subpackets)
        {
            Version = version;
            Subpackets = subpackets.ToImmutableList();
        }
    }
}
