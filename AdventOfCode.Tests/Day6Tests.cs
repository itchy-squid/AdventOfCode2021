using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Solutions.Day6;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day6Tests
    {
        readonly int[] _input = new int[] { 3, 4, 3, 1, 2 };

        [Theory]
        [InlineData(0, 5)]
        [InlineData(1, 5)]
        [InlineData(2, 6)]
        [InlineData(3, 7)]
        [InlineData(18, 26)]
        [InlineData(80, 5934)]
        [InlineData(256, 26984457539)]
        public void ProgramSolve(int day, long expected)
        {
            var result = Program.Solve(_input, day);
            Assert.Equal(expected, result);
        }
    }
}
