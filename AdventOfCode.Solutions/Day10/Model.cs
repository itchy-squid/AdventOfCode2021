using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Day10
{
    public static class Parantheses
    {
        private static ImmutableDictionary<char, char> _pairs = new Dictionary<char, char>()
        {
            { '[', ']' },
            { '(', ')' },
            { '{', '}' },
            { '<', '>' }
        }.ToImmutableDictionary();

        public static bool IsOpeningCharacter(this char c)
        {
            return _pairs.Keys.Contains(c);
        }

        public static bool MatchesOpeningCharacters(this char c, char opener)
        {
            return _pairs[opener] == c;
        }

        public static char GetClosingCharacter(this char c)
        {
            return _pairs[c];
        }
    }

    public interface IScoreBuilder
    {
        void AddResult(char? invalidChar, IEnumerable<char>? unresolvedOpeningChars);
        long ToScore();
    }

    public class Problem1ScoreBuilder : IScoreBuilder
    { 
        private static readonly ImmutableDictionary<char, int> _pointValues = new Dictionary<char, int>
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 }
        }.ToImmutableDictionary();

        long _score = 0;

        void IScoreBuilder.AddResult(char? invalidChar, IEnumerable<char>? _)
        {
            if (invalidChar != null)
            {
                _score += _pointValues[invalidChar!.Value];
            }
        }

        long IScoreBuilder.ToScore()
        {
            return _score;
        }
    }

    public class Problem2ScoreBuilder : IScoreBuilder
    {
        private static readonly ImmutableDictionary<char, long> _pointValues = new Dictionary<char, long>
        {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 }
        }.ToImmutableDictionary();

        private List<long> _scores = new();

        void IScoreBuilder.AddResult(char? invalidChar, IEnumerable<char>? unresolvedOpeningChars)
        {
            if (invalidChar == null)
            {
                _scores.Add(Score(unresolvedOpeningChars!));
            }
        }

        long Score(IEnumerable<char> chars)
        {
            long score = 0;
            foreach(char c in chars)
            {
                score = (score * 5) + _pointValues[c.GetClosingCharacter()];
            }

            return score;
        }

        long IScoreBuilder.ToScore()
        {
            var n = _scores.Count();
            return _scores.OrderBy(c => c).ElementAt(n / 2);
        }
    }
}
