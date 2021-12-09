using AdventOfCode.Solutions.Tools;
using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day8
{
    [Flags]
    public enum Panel
    {
        Top = 1,
        Middle = 2,
        Bottom = 4,
        TopLeft = 8,
        TopRight = 16,
        BottomLeft = 32,
        BottomRight = 64
    }

    public static class Digits
    {
        public static readonly Panel Zero = Panel.Top | Panel.TopLeft | Panel.TopRight | Panel.BottomLeft | Panel.BottomRight | Panel.Bottom;
        public static readonly Panel One = Panel.TopRight | Panel.BottomRight;
        public static readonly Panel Two = Panel.Top | Panel.TopRight | Panel.Middle | Panel.BottomLeft | Panel.Bottom;
        public static readonly Panel Three = Panel.Top | Panel.TopRight | Panel.Middle | Panel.BottomRight | Panel.Bottom;
        public static readonly Panel Four = Panel.TopLeft | Panel.TopRight | Panel.Middle | Panel.BottomRight;
        public static readonly Panel Five = Panel.Top | Panel.TopLeft | Panel.Middle | Panel.BottomRight | Panel.Bottom;
        public static readonly Panel Six = Five | Panel.BottomLeft;
        public static readonly Panel Seven = One | Panel.Top;
        public static readonly Panel Eight = Zero | Panel.Middle;
        public static readonly Panel Nine = Five | Panel.TopRight;

        public static readonly Panel[] All = new[] { Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine };

        public static IEnumerable<Panel> AsEnumerable(this Panel digit)
        {
            return Enum.GetValues<Panel>().Where(p => digit.HasFlag(p));
        }
    }

    public interface IModel
    {
        bool TryLearn(string token);
        int Lookup(string token);
    }

    internal class Model
    {
        Dictionary<Panel, string> _possibilitiesByPanel = Enum.GetValues<Panel>().ToDictionary(p => p, p => "abcdefg");
        
        static readonly ImmutableDictionary<int, int> _valuesByUniqueLength = Digits.All
            .Select((d, idx) => new { Idx = idx, Digit = d, NPanels = d.AsEnumerable().Count() })
            .GroupBy(tuple => tuple.NPanels)
            .Where(g => g.Count() == 1)
            .Select(g => g.First())
            .ToImmutableDictionary(tuple => tuple.NPanels, tuple => tuple.Idx);

        public bool TryLearn(string token)
        {
            if ( _valuesByUniqueLength.TryGetValue(token.Length, out var value)) 
            {
                Learn(value, token);
                return true;
            }

            throw new NotImplementedException();
        }

        private void Learn(int value, string token)
        {
            var digitLearned = Digits.All[value];
            var panelsToUpdate = Enum.GetValues<Panel>().Where(p => digitLearned.HasFlag(p));

            foreach (var panel in panelsToUpdate)
            {
                Learn(panel, token);
            }
        }

        private void Learn(Panel panel, string token)
        {
            var possibilities = _possibilitiesByPanel[panel].ToCharArray();
            var filter = token.ToCharArray();
            _possibilitiesByPanel[panel] = new(possibilities.Intersect(filter).ToArray());
        }

        public int Lookup(string token)
        {
            var panels = token.ToCharArray().Select(Panel).Aggregate((a, b) => a | b);
            var match = Digits.All
                .Select((d, idx) => new { Digit = d, Value = idx })
                .Where(tuple => tuple.Digit == panels)
                .Single();

            return match.Value;
        }

        private Panel Panel(char c)
        {
            return _possibilitiesByPanel
                .Where(kvp => kvp.Value == c.ToString())
                .Select(kvp => kvp.Key)
                .First();
        }
    }
}
