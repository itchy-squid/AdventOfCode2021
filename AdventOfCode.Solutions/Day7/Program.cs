using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day7
{
    public class Program
    {
        public delegate int FuelCalculator(int targetPosition, int crabPosition, int crabCount);

        public static void Main()
        {
            var input = Input.ReadAllLines("Day7")
                .First()
                .Split(',')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => int.Parse(s));

            int result;
            result = Solve(input, Problem1);
            Console.WriteLine($"Fuel: {result}");
            Console.WriteLine();

            result = Solve(input, Problem2);
            Console.WriteLine($"Fuel: {result}");
            Console.WriteLine();
        }

        public static int Solve(IEnumerable<int> crabPositions, FuelCalculator calculator)
        {
            var crabCountsByPosition = crabPositions.GroupBy(c => c).ToDictionary(c => c.Key, c => c.Count());
            var minPosition = crabCountsByPosition.Keys.Min();
            var maxPosition = crabCountsByPosition.Keys.Max();

            return Enumerable.Range(minPosition, maxPosition - minPosition + 1)
                .Select(n => crabCountsByPosition.Select(kvp => calculator(n, kvp.Key, kvp.Value)).Sum())
                .Min();
        }

        public static FuelCalculator Problem1 => (int targetPosition, int crabPosition, int crabCount) =>
            Math.Abs(targetPosition - crabPosition) * crabCount;

        public static FuelCalculator Problem2 => (int targetPosition, int crabPosition, int crabCount) =>
        {
            var n = Math.Abs(targetPosition - crabPosition);
            return Convert.ToInt32((n * (n + 1) / 2.0) * crabCount);
        };
    }
}
