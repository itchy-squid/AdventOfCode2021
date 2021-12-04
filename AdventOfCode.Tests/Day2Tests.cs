using AdventOfCode.Solutions.Day2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day2Tests
    {
        readonly string[] _input = new string[] 
        { 
            "forward 5/n",
            "down 5/n",
            "forward 8/n",
            "up 3/n",
            "down 8/n",
            "forward 2/n" 
        };

        [Fact]
        public void ProgramSolve_Problem1Submarine()
        {
            var sub = new Problem1Submarine();
            Program.Solve(string.Concat(_input), sub);

            Assert.Equal(15, sub.Horizontal);
            Assert.Equal(10, sub.Depth);
        }

        [Fact]
        public void ProgramSolve_Problem2Submarine()
        {
            var sub = new Problem2Submarine();
            Program.Solve(string.Concat(_input), sub);

            Assert.Equal(15, sub.Horizontal);
            Assert.Equal(60, sub.Depth);
        }
    }
}
