using AdventOfCode.Solutions.Day8;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day8Tests
    {
        readonly string _input = 
@"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce";

        [Fact]
        public void Program1Solve()
        {
            var count = Program1.Solve(_input);
            Assert.Equal(26, count);
        }

        [Fact]
        public void Program2Solve()
        {
            var sum = Program2.Solve(_input);
            Assert.Equal(61229, sum);
        }

        [Theory]
        [InlineData("be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe", 8394)]
        [InlineData("edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc", 9781)]
        [InlineData("fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg", 1197)]
        public void Program2Solve_SingleLine(string line, int expected)
        {
            var sum = Program2.SolveLine(line);
            Assert.Equal(expected, sum);
        }

        [Fact]
        public void Model_LearnsTopPanelAfterSeeing2And7()
        {
            var model = new Model();
            
            model.TryLearn("cf");
            model.TryLearn("acf");

            Assert.Equal("a", model.GetPossibilities(Panel.Top));
        }

        [Theory]
        [InlineData(Panel.TopRight, "cf")]
        [InlineData(Panel.TopLeft, "abdeg")]
        public void Model_FiltersPossibilities(Panel panel, string remainingOptions)
        {
            var model = new Model();

            model.TryLearn("cf");
            Assert.Equal(remainingOptions, model.GetPossibilities(panel));
        }

        [Fact]
        public void Model_LearnsAllPanels()
        {
            var model = new Model();

            model.TryLearn("be");
            model.TryLearn("cfbegad");
            model.TryLearn("cbdgef");
            model.TryLearn("fgaecd");
            model.TryLearn("cgeb");
            model.TryLearn("fdcge");
            model.TryLearn("agebfd");
            model.TryLearn("fecdb");
            model.TryLearn("fabcd");
            model.TryLearn("edb");

            model.TryLearn("be");
            model.TryLearn("cfbegad");
            model.TryLearn("cbdgef");
            model.TryLearn("fgaecd");
            model.TryLearn("cgeb");
            model.TryLearn("fdcge");
            model.TryLearn("agebfd");
            model.TryLearn("fecdb");
            model.TryLearn("fabcd");
            model.TryLearn("edb");

            var results = new[] {
                model.GetPossibilities(Panel.Top),
                model.GetPossibilities(Panel.TopLeft),
                model.GetPossibilities(Panel.TopRight),
                model.GetPossibilities(Panel.Middle),
                model.GetPossibilities(Panel.BottomLeft),
                model.GetPossibilities(Panel.BottomRight),
                model.GetPossibilities(Panel.Bottom)
            };

            Assert.All(results, r => Assert.Equal(1, r.Length));
        }
    }
}
