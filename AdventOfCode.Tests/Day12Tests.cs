using AdventOfCode.Solutions.Day12;
using AdventOfCode.Solutions.Tools;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day12Tests
    {
        const string _input1 = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";

        const string _input2 = @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc";

        const string _input3 = @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW";

        [Theory]
        [InlineData(_input1, 10)]
        [InlineData(_input2, 19)]
        [InlineData(_input3, 226)]
        public void ProgramSolve_Problem1(string input, int expected)
        {
            var result = Program.Solve(input.SplitLines(), Program.Problem1Create);
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(_input1, 36)]
        [InlineData(_input2, 103)]
        [InlineData(_input3, 3509)]
        public void ProgramSolve_Problem2(string input, int expected)
        {
            var result = Program.Solve(input.SplitLines(), Program.Problem2Create);
            Assert.Equal(expected, result);
        }
    }
}