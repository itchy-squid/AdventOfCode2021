namespace AdventOfCode.Solutions.Day4
{
    internal class Board
    {
        public const int Height = 5;
        public const int Width = 5;

        Space[][] _model;

        public Board(Space[][] model)
        {
            _model = model;
        }

        public static Board TryParse(IEnumerable<string> input)
        {
            return new Board(input.Select(TryParseRow).ToArray());
        }

        private static Space[] TryParseRow(string line, int y) 
        {
            return line
                .Trim()
                .Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select((n, x) => new Space { X = x, Y = y, Value = int.Parse(n) })
                .ToArray();
        }

        public bool TryWin(int move, out IEnumerable<int>? unhits)
        {
            unhits = null;

            if (TryFindValue(move, out var match))
            {
                match!.Hit = true;
                if (IsWinningBoard(match.X, match.Y))
                {
                    unhits = _model.SelectMany(s => s).Where(s => !s.Hit).Select(s => s.Value);
                    return true;
                }
            }
            
            return false;
        }

        private bool TryFindValue(int move, out Space? space)
        {
            space = _model.SelectMany(s => s).FirstOrDefault(s => s.Value == move);
            return space != null;
        }

        private bool IsWinningBoard(int x, int y)
        {
            return _model[y].All(s => s.Hit) || _model.Select(row => row[x]).All(s => s.Hit);
        }
    }

    public class Space
    {
        public int Value { get; init; }

        public bool Hit { get; set; }

        public int X { get; init; }

        public int Y { get; init; }
    }

}
