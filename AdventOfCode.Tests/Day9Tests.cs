using AdventOfCode.Solutions.Day9;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day9Tests
    {
        readonly string[] _input = new string[] 
        {
            "2199943210",
            "3987894921",
            "9856789892",
            "8767896789",
            "9899965678",
        };

        [Fact]
        public void Program1Solve_Problem1()
        {
            var result = Program1.Solve(_input);
            Assert.Equal(15, result);
        }

        [Fact]
        public void Program2Solve_Problem2()
        {
            var result = Program2.Solve(_input);
            Assert.Equal(1134, result);
        }
    }
}