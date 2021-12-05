using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Day4
{
    public delegate Win WinSelector(IEnumerable<Win> wins);

    public class Win
    {
        public int NoMoves { get; init; }
        public int WinningMove { get; init; }
        public int Unhits { get; init; }
    }

    public class Solver
    {
        public static (int, int) Solve(string movesLine, IEnumerable<string> boardLines, WinSelector pick)
        {
            var moves = movesLine
                .Split(',')
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(n => int.Parse(n))
                .ToList();

            var boards = boardLines
                .Chunk(Board.Height + 1)
                .Select(boardLines => Board.TryParse(boardLines.Take(Board.Height)))
                .ToList();

            var wins = PlayAll(moves, boards);
            var win = pick(wins);

            return (win.WinningMove, win.Unhits);
        }

        private static int Total(IEnumerable<int> values)
        {
            return values.Aggregate((a, b) => a + b);
        }

        private static IEnumerable<Win> PlayAll(IEnumerable<int> moves, IEnumerable<Board> boards)
        {
            foreach (var (move, noMove) in moves.Select((m, i) => (m, i)))
            {
                boards = boards.ToList();
                foreach (var board in boards)
                {
                    if (board.TryWin(move, out var unhits))
                    {
                        yield return new Win
                        {
                            WinningMove = move,
                            Unhits = Total(unhits!),
                            NoMoves = noMove
                        };

                        boards = boards.Except(new[] { board });
                    }
                }
            }
        }
    }
}
