using AdventOfCode.Solutions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var fishCounts = fish.GroupBy(n => n).OrderByDescending(n => n.Key).ToDictionary(n => n.Key, n => (long) n.Count());

            for(int j = 0; j <= MaxGestationPeriod; j++)
            {
                if(!fishCounts.TryGetValue(j, out var _))
                {
                    fishCounts.Add(j, 0);
                }
            }

            for(int i = 0; i < nDays; i++)
            {
                long lastValue = 0;
                for(int j = MaxGestationPeriod; j>= 0; j--)
                {
                    var temp = fishCounts[j];
                    fishCounts[j] = lastValue;
                    lastValue = temp;                    
                }
                fishCounts[MaxGestationPeriod] = lastValue;
                fishCounts[6] += lastValue;
            }

            return fishCounts.Values.Aggregate((a, b) => a + b);
        }

        public static void Dump(int nDays, long result)
        {
            Console.WriteLine($"{result} fish after {nDays} days");
            Console.WriteLine();
        }
    }
}
