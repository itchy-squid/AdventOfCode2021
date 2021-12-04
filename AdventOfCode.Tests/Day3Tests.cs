using AdventOfCode.Solutions.Day3;
using Xunit;
using System.Linq;

namespace AdventOfCode.Tests
{
    public class Day3Tests
    {
        private readonly string[] input =
        {
            "00100",
            "11110",
            "10110",
            "10111",
            "10101",
            "01111",
            "00111",
            "11100",
            "10000",
            "11001",
            "00010",
            "01010"
        };

        [Fact]
        public void Program2Solve_Oxygen()
        {
            var result = Program2.Solve(input, Program2.PickOxygen);
            Assert.Equal(23, result);
        }

        [Fact]
        public void Program2Filter_Oxygen_FirstDigit()
        {
            var result = Program2.Filter(input.Select(i => i.ToCharArray()), Program2.PickOxygen, 0);
            Assert.Equal(7, result.Count());
        }

        [Fact]
        public void Program2Solve_CO2()
        {
            var result = Program2.Solve(input, Program2.PickCO2);
            Assert.Equal(10, result);
        }

        [Fact]
        public void Program2Filter_CO2_FirstDigit()
        {
            var result = Program2.Filter(input.Select(i => i.ToCharArray()), Program2.PickCO2, 0);
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void Program2Filter_CO2_SecondDigit()
        {
            var input = new string[]
            {
                "01111", 
                "01010"
            }.Select(i => i.ToCharArray());

            var result = Program2.Filter(input, Program2.PickCO2, 1);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Program2Filter_CO2_ThirdDigit()
        {
            var input = new string[]
            {
                "01111",
                "01010"
            }.Select(i => i.ToCharArray());

            var result = Program2.Filter(input, Program2.PickCO2, 2);
            
            Assert.Equal("01010", new string(result.Single()));
        }
    }
}