using AdventOfCode.Solutions.DataStructures;
using AdventOfCode.Solutions.Tools;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Day20
{
    public class Program
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day20");

            var result = Solve(input);
            Console.WriteLine(result);
            Console.WriteLine();

            var result2 = Solve(input, 50);
            Console.WriteLine(result2);
            Console.WriteLine();
        }

        public static int Solve(IEnumerable<string> lines, int nEnhance = 2)
        {
            var (image, algorithm) = Parse(lines);

            for (var i = 0; i < nEnhance; i++)
            {
                image = image.ProcessImage(algorithm);
            }

            return image.Count();
        }

        public static (Image, ImmutableArray<int>) Parse(IEnumerable<string> lines)
        {
            Func<char, int> charToIntConverter = ch => ch == '#' ? 1 : 0;
            var algorithm = lines.First().Select(charToIntConverter).ToImmutableArray();

            var imageBits = lines
                .Skip(1)
                .SelectMany((line, y) => line.Select((ch, x) => ((Point)(x, y), charToIntConverter(ch))));

            return (new Image(imageBits, 0), algorithm);
        }
    }

    public class Image
    {
        private readonly IDefaultDictionary<Point, int> _bits;

        private readonly int _backgroundBit;

        public Image(IEnumerable<(Point, int)> image, int backgroundBit)
        {
            _backgroundBit = backgroundBit;
            _bits = image.ToDefaultDictionary(tuple => tuple.Item1, tuple => tuple.Item2, () => _backgroundBit);
        }

        public Image ProcessImage(ImmutableArray<int> algorithm)
        {
            var nextBackgroundBit = algorithm[Enumerable.Repeat(_backgroundBit, 9).ToInt()];
            var nextBits = new List<(Point, int)>();

            var minX = _bits.Select(kvp => kvp.Key.X).Min() - 1;
            var maxX = _bits.Select(kvp => kvp.Key.X).Max() + 1;

            var minY = _bits.Select(kvp => kvp.Key.Y).Min() - 1;
            var maxY = _bits.Select(kvp => kvp.Key.Y).Max() + 1;

            for(var x = minX; x <= maxX; x++)
            {
                for(var y = minY; y <= maxY; y++)
                {
                    var algorithmIndex = Enumerable.Range(y - 1, 3)
                        .SelectMany(j => Enumerable.Range(x - 1, 3).Select(i => (i, j)))
                        .Select(pt => _bits[pt])
                        .ToInt();

                    nextBits.Add(((x, y), algorithm[algorithmIndex]));
                }
            }

            return new Image(nextBits, nextBackgroundBit);
        }

        public override string ToString()
        {
            var minX = _bits.Select(kvp => kvp.Key.X).Min() - 1;
            var maxX = _bits.Select(kvp => kvp.Key.X).Max() + 1;

            var minY = _bits.Select(kvp => kvp.Key.Y).Min() - 1;
            var maxY = _bits.Select(kvp => kvp.Key.Y).Max() + 1;

            StringBuilder builder = new();

            for (var y = minY; y <= maxY; y++)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    builder.Append(_bits[(x, y)] == 1 ? '#' : '.');
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        public int Count()
        {
            return _backgroundBit == 0 
                ? _bits.Values.Where(i => i == 1).Count() 
                : int.MaxValue;
        }
    }

    public static class BitStreamExtensions
    {
        public static int ToInt(this IEnumerable<int> bitStream)
        {
            var bits = bitStream.ToList();
            var nBits = bits.Count;
            return bits.Select((b, i) => b << (nBits - i - 1)).Aggregate(0, (a, b) => a | b);
        }
    }
}
