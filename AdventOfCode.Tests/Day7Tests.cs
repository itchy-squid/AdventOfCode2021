using AdventOfCode.Solutions.Day7;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day7Tests
    {
        readonly IEnumerable<int> _input = new[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };

        [Fact]
        public void ProgramSolve_Problem1()
        {
            var fuel = Program.Solve(_input, Program.Problem1);
            Assert.Equal(37, fuel);
        }

        [Fact]
        public void ProgramSolve_Problem2()
        {
            var fuel = Program.Solve(_input, Program.Problem2);
            Assert.Equal(168, fuel);
        }
    }
}
