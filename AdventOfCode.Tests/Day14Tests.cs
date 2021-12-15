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
        [InlineData(1, "NCNBCHB")]
        [InlineData(2, "NBCCNBBBCBHCB")]
        [InlineData(3, "NBBBCNCCNBBNBNBBCHBHHBCHB")]
        [InlineData(4, "NBBNBNBBCCNBCNCCNBBNBBNBBBNBBNBBCBHCBHHNHCBBCBHCB")]
        public void ProgramSolve(int steps, string expected)
        {
            var result = Program.Solve(_input.SplitLines(), steps, Program.NoOp);
            Assert.Equal(expected, result);
        }
        /*After step 5, it has length 97; After step 10, it has length 3073. After step 10, B occurs 1749 times, C occurs 298 times, H occurs 161 times, and N occurs 865 times; taking the quantity of the most common element (B, 1749) and subtracting the quantity of the least common element (H, 161) produces 1749 - 161 = 1588.*/

        [Fact]
        public void ProgramSolve_Length()
        {
            var result = Program.Solve(_input.SplitLines(), 5, Program.Length);
            Assert.Equal(97, result);
        }

        [Theory]
        [InlineData(10, 1588)]
        //[InlineData(40, 2188189693529)]
        public void ProgramSolve_MostCommonMinusLeastCommon(int steps, long expected)
        {
            var result = Program.Solve(_input.SplitLines(), steps, Program.MostCommonMinusLeastCommon);
            Assert.Equal(expected, result);
        }
    }
}