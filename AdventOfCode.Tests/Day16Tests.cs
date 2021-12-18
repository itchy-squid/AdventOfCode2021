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
        [InlineData("EE00D40C823060", 12)]
        [InlineData("8A004A801A8002F478", 16)]
        [InlineData("620080001611562C8802118E34", 12)]
        [InlineData("C0015000016115A2E0802F182340", 23)]
        [InlineData("A0016C880162017C3686B18A3D4780", 31)]
        public void ProgramSolve_Problem1(string input, int expected)
        {
            var result = Program.Solve(input);
            Assert.Equal(expected, result);
        }
    }
}