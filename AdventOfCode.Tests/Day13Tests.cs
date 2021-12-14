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

        [Fact]
        public void ProgramSolve_Problem1()
        {
            var result = Program.Solve(_input.SplitLines());
            Assert.Equal(17, result);
        }
    }
}