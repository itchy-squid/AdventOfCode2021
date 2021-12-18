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

    public class OperatorPacket : IPacket
    {
        public int Version { get; }

        public PacketTypes Type { get; }

        public ImmutableArray<IPacket> Subpackets { get; }

        IEnumerable<IPacket> IPacket.Subpackets => Subpackets;

        public OperatorPacket(int version, int type, IEnumerable<IPacket> subpackets)
        {
            Version = version;
            Type = (PacketTypes)type;
            Subpackets = subpackets.ToImmutableArray();
        }

        public long Evaluate()
        {
            return (Type) switch
            {
                PacketTypes.SUM => Subpackets.Select(p => p.Evaluate()).Sum(),
                PacketTypes.PRODUCT => Subpackets.Select(p => p.Evaluate()).Aggregate((a, b) => a * b),
                PacketTypes.MIN => Subpackets.Select(p => p.Evaluate()).Min(),
                PacketTypes.MAX => Subpackets.Select(p => p.Evaluate()).Max(),
                PacketTypes.LT => Subpackets[0].Evaluate() < Subpackets[1].Evaluate() ? 1 : 0,
                PacketTypes.GT => Subpackets[0].Evaluate() > Subpackets[1].Evaluate() ? 1 : 0,
                PacketTypes.EQ => Subpackets[0].Evaluate() == Subpackets[1].Evaluate() ? 1 : 0,

                _ => throw new NotImplementedException()
            };
        }
    }
}
