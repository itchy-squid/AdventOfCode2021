using AdventOfCode.Solutions.Tools;
using System.Collections.Immutable;

namespace AdventOfCode.Solutions.Day8
{
    [Flags]
    public enum Panel
    {
        UnsetValue = 0,
        Top = 1,
        Middle = 2,
        Bottom = 4,
        TopLeft = 8,
        TopRight = 16,
        BottomLeft = 32,
        BottomRight = 64
    }

    public static class Panels
    {
        public static readonly Panel None = Panel.UnsetValue;
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

        public static readonly ImmutableArray<Panel> ByValue = new[] { Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine }.ToImmutableArray();

        public static IEnumerable<Panel> AsEnumerable(this Panel digit)
        {
            return Panels.AsEnumerable().Where(p => digit.HasFlag(p));
        }

        public static IEnumerable<Panel> AsEnumerable()
        {
            return Enum.GetValues<Panel>().Where(p => p != Panel.UnsetValue);
        }
    }

    public class Digit
    {
        public int Value { get; init; }
        public Panel Display { get; init; }
        public int Length { get; init; }
    }

    public static class Digits
    {
        public static readonly ImmutableList<Digit> All = Panels.ByValue
            .Select((d, idx) => new Digit() { Value = idx, Display = d, Length = d.AsEnumerable().Count() })
            .ToImmutableList();
    }

    public class Model
    {
        Dictionary<Panel, string> _possibilitiesByPanel = Enum.GetValues<Panel>().ToDictionary(p => p, p => "abcdefg");
        
        public bool TryLearn(string token)
        {
            var filters = new[]
            {
                FilterByLength,
                FilterByPigeonHole
            };

            IEnumerable<Digit> possibilities = Digits.All;
            foreach(var filter in filters)
            {
                possibilities = filter(possibilities, token).ToList();
                if(possibilities.Count() == 1)
                {
                    Learn(possibilities.Single().Value, token);
                    return true;
                }
            }

            return false;
        }

        private IEnumerable<Digit> FilterByLength(IEnumerable<Digit> digits, string token)
        {
            return digits.Where(d => d.Length == token.Length);
        }

        private IEnumerable<Digit> FilterByPigeonHole(IEnumerable<Digit> possibilities, string token)
        {
            var tokenCharacters = token.ToCharArray();

            var pigeonsInHoles = _possibilitiesByPanel.GroupBy(kvp => kvp.Value)
                .Where(g => g.Count() == g.Key.Length)
                .Select(g => new { Substring = g.Key.ToCharArray(), Panels = g.Select(kvp => kvp.Key).Aggregate((a, b) => a | b) })
                .ToList();

            var knownPanels = pigeonsInHoles
                .Where(p => p.Substring.All(c => tokenCharacters.Contains(c)))
                .Select(p => p.Panels)
                .Aggregate(Panels.None, (a, b) => a | b);

            return possibilities.Where(p => knownPanels != Panels.None && (p.Display & knownPanels) == knownPanels);
        }

        private void Learn(int value, string token)
        {
            var digitLearned = Panels.ByValue[value];

            Panels.AsEnumerable().Where(p => digitLearned.HasFlag(p)).ForEach(p => Whitelist(p, token));
            Panels.AsEnumerable().Where(p => !digitLearned.HasFlag(p)).ForEach(p => Blacklist(p, token));
        }

        private void Whitelist(Panel panel, string token)
        {
            UpdatePossibilities(panel, token, (tokenCharacters, c) => tokenCharacters.Contains(c));
        }

        private void Blacklist(Panel panel, string token)
        {
            UpdatePossibilities(panel, token, (tokenCharacters, c) => !tokenCharacters.Contains(c));
        }

        private void UpdatePossibilities(Panel panel, string token, Func<char[], char, bool> selector)
        {
            var possibilities = _possibilitiesByPanel[panel];
            var filter = token.ToCharArray();
            possibilities = new(possibilities.ToCharArray().Where(c => selector(filter, c)).ToArray());
            _possibilitiesByPanel[panel] = possibilities;

            if (string.IsNullOrEmpty(possibilities)) throw new InvalidOperationException();
        }

        public string GetPossibilities(Panel panel)
        {
            return _possibilitiesByPanel[panel];
        }

        public int Lookup(string token)
        {
            var panels = token.ToCharArray().Select(Panel).Aggregate((a, b) => a | b);
            var match = Panels.ByValue
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
