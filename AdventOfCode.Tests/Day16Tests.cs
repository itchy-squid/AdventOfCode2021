using AdventOfCode.Solutions.Day16;
using AdventOfCode.Solutions.Tools;
using System.Linq;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day16Tests
    {
        const string _input1 = "8A004A801A8002F478";

        [Theory]
        [InlineData("0","0000")]
        [InlineData("7", "0111")]
        [InlineData("D", "1101")]
        public void ToBitStream(string hex, string bits)
        {
            var expected = bits.Select(c => c.ToInt()).ToArray();
            var result = hex.ToBitStream().ToArray();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("D2FE28", 6)]
        [InlineData("38006F45291200", 9)]
        [InlineData("EE00D40C823060", 14)]
        [InlineData("8A004A801A8002F478", 16)]
        [InlineData("620080001611562C8802118E34", 12)]
        [InlineData("C0015000016115A2E0802F182340", 23)]
        [InlineData("A0016C880162017C3686B18A3D4780", 31)]
        public void ProgramSolve_Problem1(string input, int expected)
        {
            var result = Program.Solve(input, Program.Problem1);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("D2FE28", 2021)]
        [InlineData("C200B40A82", 3)]
        [InlineData("04005AC33890", 54)]
        [InlineData("880086C3E88112", 7)]
        [InlineData("CE00C43D881120", 9)]
        [InlineData("D8005AC2A8F0", 1)]
        [InlineData("F600BC2D8F", 0)]
        [InlineData("9C005AC2F8F0", 0)]
        [InlineData("9C0141080250320F1802104A08", 1)]
        public void ProgramSolve_Problem2(string input, int expected)
        {
            var result = Program.Solve(input, Program.Problem2);
            Assert.Equal(expected, result);
        }
    }
}