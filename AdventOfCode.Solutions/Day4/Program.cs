using AdventOfCode.Solutions.Services;

namespace AdventOfCode.Solutions.Day4
{
    public class Program
    {
        public const string InputDirectory = "Day4";

        public delegate Win WinSelector(IEnumerable<Win> wins);
        public static readonly WinSelector Problem1 = (wins) => wins.First();
        public static readonly WinSelector Problem2 = (wins) => wins.Last();

        public static void Main()
        {
            int win, hits;
            var input = Input.ReadAllLines(InputDirectory);
            
            (win, hits) = Solve(input.First(), input.Skip(2), Problem1);
            Dump(win, hits);

            (win, hits) = Solve(input.First(), input.Skip(2), Problem2);
            Dump(win, hits);
        }

        public static (int,int) Solve(string movesLine, IEnumerable<string> boardLines, WinSelector pick)
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

        private static void Dump(int move, int unhits)
        {
            Console.WriteLine($"Win: {move}");
            Console.WriteLine($"Hits: {unhits}");
            Console.WriteLine($"Win x Hits: {move * unhits}");
            Console.WriteLine();
        }

        public class Win
        {
            public int NoMoves { get; init; }
            public int WinningMove { get; init; }
            public int Unhits { get; init; }
        }
    }
}
