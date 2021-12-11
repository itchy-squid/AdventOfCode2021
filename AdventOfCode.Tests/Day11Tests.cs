using AdventOfCode.Solutions.Day11;
using AdventOfCode.Solutions.Tools;
using Xunit;

public class Day11Tests
{
    private const string _input = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";

    [Fact]
    public void ProgramSolve_Problem1()
    {
        var result = Program.Solve(_input.SplitLines(), Program.Problem1);
        Assert.Equal(1656, result);
    }

    [Fact]
    public void ProgramSolve_Problem2()
    {
        var result = Program.Solve(_input.SplitLines(), Program.Problem2);
        Assert.Equal(195, result);
    }
}