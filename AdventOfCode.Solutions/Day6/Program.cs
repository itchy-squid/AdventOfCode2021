using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day6
{
    public class Program
    {
        const int MaxGestationPeriod = 8;

        public static void Main()
        {
            var input = Input.ReadAllLines("Day6");
            var fish = input.First().Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => int.Parse(s)).ToList();

            long result;
            result = Solve(fish, 80);
            Dump(80, result);

            result = Solve(fish, 256);
            Dump(256, result);
        }

        public static long Solve(IEnumerable<int> fish, int nDays)
        {
            var fishCountsByAge = fish.GroupBy(n => n).OrderByDescending(n => n.Key).ToDictionary(n => n.Key, n => (long)n.Count());

            for (int j = 0; j <= MaxGestationPeriod; j++)
            {
                if (!fishCountsByAge.TryGetValue(j, out var _))
                {
                    fishCountsByAge.Add(j, 0);
                }
            }

            for (int i = 0; i < nDays; i++)
            {
                long lastValue = 0;
                for(int j = MaxGestationPeriod; j>= 0; j--)
                {
                    (fishCountsByAge[j], lastValue) = (lastValue, fishCountsByAge[j]);
                    //var temp = fishCountsByAge[j];
                    //fishCountsByAge[j] = lastValue;
                    //lastValue = temp;
                }
                fishCountsByAge[MaxGestationPeriod] = lastValue;
                fishCountsByAge[6] += lastValue;
            }

            return fishCountsByAge.Values.Aggregate((a, b) => a + b);
        }

        public static void Dump(int nDays, long result)
        {
            Console.WriteLine($"{result} fish after {nDays} days");
            Console.WriteLine();
        }
    }
}
