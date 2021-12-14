using AdventOfCode.Solutions.Day13;
using AdventOfCode.Solutions.Tools;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day13Tests
    {
        const string _input = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";

        const string _output = @"#####
#...#
#...#
#...#
#####";

        [Theory]
        [InlineData(1, 17)]
        [InlineData(null, 16)]
        public void ProgramProblem1Solve(int? steps, int expected)
        {
            var result = Program.Problem1Solve(_input.SplitLines(), steps);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ProgramProblem2Solve()
        {
            var result = Program.Problem2Solve(_input.SplitLines());
            Assert.Equal(_output, result);
        }
    }
}