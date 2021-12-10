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
        public void ProgramSolve_Problem1()
        {
            var result = Program.Solve(_input);
            Assert.Equal(15, result);
        }
    }
}