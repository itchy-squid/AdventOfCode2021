using AdventOfCode.Solutions.Day18;
using AdventOfCode.Solutions.Tools;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day18Tests
    {
        const string _input1 = @"[1,1]
[2,2]
[3,3]
[4,4]";

        const string _input2 = @"[1,1]
[2,2]
[3,3]
[4,4]
[5,5]";

        const string _input3 = @"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]
[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]
[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]
[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]
[7,[5,[[3,8],[1,4]]]]
[[2,[2,2]],[8,[8,1]]]
[2,9]
[1,[[[9,3],9],[[9,0],[0,7]]]]
[[[5,[7,4]],7],1]
[[[[4,2],2],6],[8,7]]";

        const string _input4 = @"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]";

        [Theory]
        [InlineData("[[[[0,9],2],3],4]")]
        public void ParserParse_SingleLine(string input)
        {
            var model = Parser.Parse(input);
            Assert.Equal(input, model.ToString());
        }

        [Theory]
        [InlineData(_input1, "[[[[1,1],[2,2]],[3,3]],[4,4]]")]
        [InlineData(_input2, "[[[[3,0],[5,3]],[4,4]],[5,5]]")]
        [InlineData(_input3, "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]")]
        [InlineData(_input4, "[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]")]
        public void ParserParse_MultipleLines(string input, string expected)
        {
            var model = Parser.Parse(input.SplitLines());
            Assert.Equal(expected, model.ToString());
        }

        [Fact]
        public void SnailfishAdd()
        {
            var a = Parser.Parse("[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]");
            var b = Parser.Parse("[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]");
            var result = a + b;

            Assert.Equal("[[[[7,8],[6,6]],[[6,0],[7,7]]],[[[7,8],[8,8]],[[7,9],[0,6]]]]", result.ToString());
            Assert.Equal(3993, result.Magnitude());
        }

        [Theory]
        [InlineData("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
        [InlineData("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
        [InlineData("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
        [InlineData("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
        public void SnailfishNumberExplode(string input, string expected)
        {
            var model = Parser.Parse(input);

            Assert.True(model.TryExplode());
            Assert.Equal(expected, model.ToString());
        }

        [Fact]
        public void SnailfishNumberSplit()
        {
            var model = new SnailfishOperator()
            {
                Left = new SnailfishLiteral(1),
                Right = new SnailfishLiteral(15)
            };

            Assert.True(model.TrySplit());
            Assert.Equal("[1,[7,8]]", model.ToString());
        }

        [Fact]
        public void ProgramProblem1Solve()
        {
            var result = Program.Problem1Solve(_input4.SplitLines());
            Assert.Equal(4140, result);
        }

        [Fact]
        public void ProgramProblem2Solve()
        {
            var result = Program.Problem2Solve(_input4.SplitLines());
            Assert.Equal(3993, result);
        }
    }
}
