using AdventOfCode.Solutions.Day17;
using AdventOfCode.Solutions.Tools;
using System.Linq;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day17Tests
    {
        const string _input = "target area: x=20..30, y=-10..-5";

        [Fact]
        public void ProgramSolve_Problem1()
        {
            var result = Program.Solve(_input, Program.Problem1);
            Assert.Equal(45, result);
        }

        [Fact]
        public void ProgramSolve_Problem2()
        {
            var result = Program.Solve(_input, Program.Problem2);
            Assert.Equal(112, result);
        }
    }
}