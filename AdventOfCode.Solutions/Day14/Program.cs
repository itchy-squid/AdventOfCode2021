using AdventOfCode.Solutions.Tools;
using System.Collections.Immutable;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Day14
{
    public static class Program
    {
        public static string NoOp(LinkedList<char> polymer) => new string(polymer.ToArray());
        public static int Length(LinkedList<char> polymer) => polymer.Count();
        public static long MostCommonMinusLeastCommon(LinkedList<char> polymer) 
        {
            var charFrequencies = polymer
                .GroupBy(ch => ch)
                .Select(ch => new { Character = ch, Count = ch.LongCount() })
                .OrderBy(ch => ch.Count)
                .ToArray();

            return charFrequencies[charFrequencies.Length - 1].Count - charFrequencies[0].Count;
        }
            

        public static void Main()
        {
            var input = Input.ReadAllLines("Day14");

            var result1 = Solve(input, 10, MostCommonMinusLeastCommon);
            Console.WriteLine(result1);
            Console.WriteLine();

            var result2 = Solve(input, 40, MostCommonMinusLeastCommon);
            Console.WriteLine(result2);
            Console.WriteLine();
        }

        public static T Solve<T>(IEnumerable<string> lines, int steps, Func<LinkedList<char>, T> permute)
        {
            var polymer = Template(lines.First());
            var rules = Rules(lines.Skip(2)).ToImmutableList();

            for(var i = 0; i < steps; i++)
            {
                var ruleApplications = rules.SelectMany(r => r.Match(polymer)).ToList();
                ruleApplications.ForEach(r => r());
            }

            return permute(polymer);
        }

        private static LinkedList<char> Template(string line)
        {
            return new LinkedList<char>(line.Trim().ToCharArray());
        }

        private static IEnumerable<Rule> Rules(IEnumerable<string> lines)
        {
            return lines.Select(rule => new Rule(rule.Substring(0, 2), rule[6]));
        }

    }

    public class Rule
    {
        public string Pattern { get; init; }
        public char InjectionElement { get; init; }

        public Rule(string pattern, char injectionElement)
        {
            Pattern = pattern;
            InjectionElement = injectionElement;
        }

        public IEnumerable<Action> Match(LinkedList<char> polymer)
        {
            for(var curr = polymer.First;  curr != null; curr = curr!.Next)
            {
                if (curr.Next == null) break;
                if (curr.Value != Pattern[0] || curr.Next.Value != Pattern[1]) continue;
                
                yield return DelayedAddAfter(polymer, curr, InjectionElement);
            }
        }

        private Action DelayedAddAfter(LinkedList<char> list, LinkedListNode<char> node, char element)
        {
            return () =>
            {
                list.AddAfter(node, element);
            };
        }
    }
}
