﻿using AdventOfCode.Solutions.Day4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day4Tests
    {
        const string moves = @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1";
        static readonly string[] boards = new string[]
        {
            "22 13 17 11  0",
            " 8  2 23  4 24",
            "21  9 14 16  7",
            " 6 10  3 18  5",
            " 1 12 20 15 19",
            "              ",
            " 3 15  0  2 22",
            " 9 18 13 17  5",
            "19  8  7 25 23",
            "20 11 10 24  4",
            "14 21 16 12  6",
            "              ",
            "14 21 17 24  4",
            "10 16 15  9 19",
            "18  8 23 26 20",
            "22 11 13  6  5",
            " 2  0 12  3  7"
        };

        [Fact]
        public void ProgramSolve_Problem1()
        {
            var (win, hits) = Program.Solve(moves, boards, Program.Problem1);
            Assert.Equal(4512, win * hits);
        }

        [Fact]
        public void ProgramSolve_Problem2()
        {
            var (win, hits) = Program.Solve(moves, boards, Program.Problem2);
            Assert.Equal(1924, win * hits);
        }

    }
}
