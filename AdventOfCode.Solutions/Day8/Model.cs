﻿using AdventOfCode.Solutions.Tools;
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

        public static readonly ImmutableArray<Panel> Range = new[] { Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine }.ToImmutableArray();

        public static IEnumerable<Panel> AsEnumerable(this Panel digit)
        {
            return Enum.GetValues<Panel>().Where(p => digit.HasFlag(p));
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
        public static readonly ImmutableList<Digit> All = Panels.Range
            .Select((d, idx) => new Digit() { Value = idx, Display = d, Length = d.AsEnumerable().Count() })
            .ToImmutableList();

        public static IEnumerable<Digit> FilterByLength(this IEnumerable<Digit> digits, int length)
        {
            return digits.Where(d => d.Length == length);
        }
    }

    public interface IModel
    {
        bool TryLearn(string token);
        int Lookup(string token);
        string GetPossibilities(Panel panel);
    }

    public class Model : IModel
    {
        Dictionary<Panel, string> _possibilitiesByPanel = Enum.GetValues<Panel>().ToDictionary(p => p, p => "abcdefg");
        
        public bool TryLearn(string token)
        {
            IEnumerable<Digit> possibilities = Digits.All;

            possibilities = possibilities
                .FilterByLength(token.Length)
                .ToList();

            if (possibilities.Count() == 1) 
            {
                Learn(possibilities.First().Value, token);
                return true;
            }

            possibilities = FilterByPigeonHole(possibilities, token).ToList();
            if (possibilities.Count() == 1)
            {
                Learn(possibilities.First().Value, token);
                return true;
            }

            return false;
        }

        private IEnumerable<Digit> FilterByPigeonHole(IEnumerable<Digit> possibilities, string token)
        {
            var tokenCharacters = token.ToCharArray();

            var pigeonsInHoles = _possibilitiesByPanel.GroupBy(kvp => kvp.Value)
                .Where(g => g.Count() == g.Key.Length)
                .Select(g => new { Substring = g.Key.ToCharArray(), Panels = g.Select(kvp => kvp.Key).Aggregate((a, b) => a | b) })
                .ToList();

            if (pigeonsInHoles.Any())
            {
                var knownPanels = pigeonsInHoles
                    .Where(p => p.Substring.All(c => tokenCharacters.Contains(c)))
                    .Select(p => (Panel?)p.Panels)
                    .Aggregate((a, b) => a | b);

                possibilities = possibilities.Where(p => (p.Display & knownPanels) == knownPanels);
            }

            return possibilities;
        }

        private void Learn(int value, string token)
        {
            var digitLearned = Panels.Range[value];

            Enum.GetValues<Panel>().Where(p => digitLearned.HasFlag(p)).ForEach(p => Whitelist(p, token));
            Enum.GetValues<Panel>().Where(p => !digitLearned.HasFlag(p)).ForEach(p => Blacklist(p, token));
        }

        private void Whitelist(Panel panel, string token)
        {
            var possibilities = _possibilitiesByPanel[panel].ToCharArray();
            var filter = token.ToCharArray();
            _possibilitiesByPanel[panel] = new(possibilities.Intersect(filter).ToArray());
        }

        private void Blacklist(Panel panel, string token)
        {
            var possibilities = _possibilitiesByPanel[panel].ToCharArray();
            var filter = token.ToCharArray();
            _possibilitiesByPanel[panel] = new(possibilities.Where(c => !filter.Contains(c)).ToArray());
        }

        public string GetPossibilities(Panel panel)
        {
            return _possibilitiesByPanel[panel];
        }

        public int Lookup(string token)
        {
            var panels = token.ToCharArray().Select(Panel).Aggregate((a, b) => a | b);
            var match = Panels.Range
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
