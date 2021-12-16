using AdventOfCode.Solutions.Day14;
using AdventOfCode.Solutions.Tools;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day14Tests
    {
        const string _input = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 1)]
        [InlineData(2, 5)]
        [InlineData(10, 1588)]
        [InlineData(40, 2188189693529)]
        public void ProgramSolve_MostCommonMinusLeastCommon(int steps, long expected)
        {
            var result = Program.Solve(_input.SplitLines(), steps);
            Assert.Equal(expected, result);
        }
    }
}