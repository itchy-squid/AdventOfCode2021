using AdventOfCode.Solutions.Services;

namespace AdventOfCode.Solutions.Day4
{
    public class Program1
    {
        public const string InputDirectory = "Day4";

        public static void Main()
        {
            var input = Input.ReadAllLines(InputDirectory);
            var (win, hits) = Solve(input.First(), input.Skip(2));
            Console.WriteLine($"Win: {win}");
            Console.WriteLine($"Hits: {hits}");
            Console.WriteLine($"Win x Hits: {win * hits}");
        }

        public static (int,int) Solve(string movesLine, IEnumerable<string> boardLines)
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

            foreach (var move in moves)
            foreach (var board in boards)
            {
                if (board.TryWin(move, out var unhits))
                {
                    return (move, Total(unhits!));
                }
            }

            return (0, 0);
        }

        private static int Total(IEnumerable<int> values)
        {
            return values.Aggregate((a, b) => a + b);
        }
    }
}
