using AdventOfCode.Solutions.Day20;
using AdventOfCode.Solutions.Tools;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day20Tests
    {
        const string _input = @"..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#

#..#.
#....
##..#
..#..
..###";

        [Fact]
        public void ProgramSolve_Problem1()
        {
            var result = Program.Solve(_input.SplitLines());
            Assert.Equal(35, result);
        }

        [Fact]
        public void ProgramSolve_Problem2()
        {
            var result = Program.Solve(_input.SplitLines(), 50);
            Assert.Equal(3351, result);
        }

        [Fact]
        public void ProgramParse()
        {
            var expected = @".......
.#..#..
.#.....
.##..#.
...#...
...###.
.......
";

            var (image, _) = Program.Parse(_input.SplitLines());
            Assert.Equal(expected, image.ToString());
        }

        [Fact]
        public void ImageEnhance_1x()
        {
            var expected = @".........
..##.##..
.#..#.#..
.##.#..#.
.####..#.
..#..##..
...##..#.
....#.#..
.........
";

            var (image0, alg) = Program.Parse(_input.SplitLines());
            var image1 = image0.ProcessImage(alg);

            Assert.Equal(expected, image1.ToString());
        }
    }
}
