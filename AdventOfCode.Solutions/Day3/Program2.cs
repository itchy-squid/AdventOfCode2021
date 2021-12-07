using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day3
{
    public class Program2
    {
        public const string InputDirectory = "Day3";
        public delegate T Selector<T>(IEnumerable<T> list);

        public static readonly Selector<IGrouping<char, char>> PickOxygen = g => g.Last();
        public static readonly Selector<IGrouping<char, char>> PickCO2 = g => g.First();

        public static void Main()
        {
            var lines = Input.ReadAllLines(InputDirectory);
            var oxygen = Solve(lines, PickOxygen);
            var co2 = Solve(lines, PickCO2);

            Console.WriteLine($"oxygen: {Convert.ToString(oxygen, 2)}");
            Console.WriteLine($"co2: {Convert.ToString(co2, 2)}");
            Console.WriteLine($"oxygen x co2 = {oxygen * co2}");
        }

        public static int Solve(IEnumerable<string> lines, Selector<IGrouping<char, char>> pick)
        {
            var charLines = lines.Select(line => line.ToCharArray()).ToList();
            var nDigits = charLines.First().Length;

            IEnumerable<char[]> filteredLines = charLines;
            for(int i = 0; i < nDigits && filteredLines.Count() != 1; i++)
            {
                filteredLines = Filter(filteredLines, pick, i);
            }

            var value = Convert.ToInt32(new string(filteredLines.Single()), 2);
            return value;
        }

        public static List<char[]> Filter(IEnumerable<char[]> lines, Selector<IGrouping<char, char>> pick, int i)
        {
            // 1. Figure out which character is most/least common
            var sortedCharGroups = lines.Select(line => line[i]).GroupBy(c => c).OrderBy(g => g.Count()).ToList();

            // Special case is special. (ಥ_ಥ)
            if (sortedCharGroups.Count == 2 && sortedCharGroups[0].Count() == sortedCharGroups[1].Count())
            {
                sortedCharGroups = sortedCharGroups.OrderBy(g => g.First()).ToList();
            }

            var bestChar = pick(sortedCharGroups).First();

            // 2. Filter the list
            return lines.Where(line => line[i] == bestChar).ToList();
        }
    }
}
