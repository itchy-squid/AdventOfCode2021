using AdventOfCode.Solutions.Day5;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day5Tests
    {
        const string input = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

        [Fact]
        void ProgramSolve_Problem1()
        {
            var result = Program.Solve(input.Split("\r\n"), Program.Problem1);
            Assert.Equal(5, result);
        }

        [Fact]
        void ProgramSolve_Problem2()
        {
            var result = Program.Solve(input.Split("\r\n"), Program.Problem2);
            Assert.Equal(12, result);
        }
    }
}
