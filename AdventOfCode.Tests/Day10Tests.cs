using AdventOfCode.Solutions.Day10;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day10Tests
    {
        readonly string[] _input = new string[] 
        {
            @"[({(<(())[]>[[{[]{<()<>>",
            "[(()[<>])]({[<{<<[]>>(",
            "{([(<{}[<>[]}>{[]{[(<()>",
            "(((({<>}<{<{<>}{[]{[]{}",
            "[[<[([]))<([[{}[[()]]]",
            "[{[{({}]{}}([{[{{{}}([]",
            "{<[[]]>}<{[{[{[]{()[[[]",
            "[<(<(<(<{}))><([]([]()",
            "<{([([[(<>()){}]>(<<{{",
            "<{([{{}}[<[[[<>{}]]]>[]]" 
        };

        [Fact]
        public void ProgramSolve_Problem1()
        {
            var result = Program.Solve(_input, new Problem1ScoreBuilder());
            Assert.Equal(26397, result);
        }

        [Fact]
        public void ProgramSolveLine_Problem1()
        {
            var result = Program.SolveLine("{([(<{}[<>[]}>{[]{[(<()>");
            Assert.Equal('}',result!.Item1);
        }

        [Fact]
        public void ProgramSolve_Problem2()
        {
            var result = Program.Solve(_input, new Problem2ScoreBuilder());
            Assert.Equal(288957, result);
        }
    }
}