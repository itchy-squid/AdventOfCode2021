using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day4
{
    public class Program
    {
        public const string InputDirectory = "Day4";

        public static readonly WinSelector Problem1 = (wins) => wins.First();
        public static readonly WinSelector Problem2 = (wins) => wins.Last();

        public static void Main()
        {
            int win, hits;
            var input = Input.ReadAllLines(InputDirectory);
            
            (win, hits) = Solver.Solve(input.First(), input.Skip(2), Problem1);
            Dump(win, hits);

            (win, hits) = Solver.Solve(input.First(), input.Skip(2), Problem2);
            Dump(win, hits);
        }


        private static void Dump(int move, int unhits)
        {
            Console.WriteLine($"Win: {move}");
            Console.WriteLine($"Hits: {unhits}");
            Console.WriteLine($"Win x Hits: {move * unhits}");
            Console.WriteLine();
        }
    }
}
