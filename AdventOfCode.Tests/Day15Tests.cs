using AdventOfCode.Solutions.Day15;
using AdventOfCode.Solutions.Tools;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day15Tests
    {
        const string _input = @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";

        const string _input2x2 = @"81
36";

        [Theory]
        [InlineData(_input2x2, 7)]
        [InlineData(_input, 40)]
        public void ProgramSolve_Problem1(string input, int expected)
        {
            var result = Program.Solve(input.SplitLines(), Program.Problem1);
            Assert.Equal(expected, result);
        }

        [Theory]
        //[InlineData(_input2x2, 7)]
        [InlineData(_input, 315)]
        public void ProgramSolve_Problem2(string input, int expected)
        {
            var result = Program.Solve(input.SplitLines(), Program.Problem2);
            Assert.Equal(expected, result);
        }
    }
}