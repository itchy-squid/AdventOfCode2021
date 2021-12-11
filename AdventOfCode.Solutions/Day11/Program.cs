using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day11
{
    public static class Program
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day11");

            int result;
            result = Solve(input, Problem1);
            Console.WriteLine($"No. of flashes: {result}");
            Console.WriteLine();

            result = Solve(input, Problem2);
            Console.WriteLine($"No. of flashes: {result}");
            Console.WriteLine();
        }

        public static int Solve(IEnumerable<string> input, Func<Octopus[][], int> calculator)
        {
            var octopuses = input
                .Select((row, y) =>
                    row.Select((octopus, x) => new Octopus(x, y, octopus.ToInt())).ToArray())
                .ToArray();

            return calculator(octopuses);
        }
        public static int Problem1(Octopus[][] octos)
        {
            return Enumerable.Range(0, 100).Select(i => Step(octos)).Sum();
        }

        public static int Problem2(Octopus[][] octos)
        {
            var i = 0;
            while(!octos.SelectMany(row => row).All(o => o.Lumens == 0))
            {
                Step(octos);
                i++;
            }

            return i;
        }

        public static int Step(Octopus[][] octos)
        {
            var height = octos.Length;
            var width = octos[0].Length;

            while(octos.SelectMany(row => row).Any(o => o.WillFlash()))
            {
                var octo = octos.SelectMany(row => row).First(o => o.WillFlash());
                octo.Flash();

                var (x, y) = (octo.X, octo.Y);
                TryReceiveFlash(x - 1, y - 1, width, height, octos);
                TryReceiveFlash(x - 1, y, width, height, octos);
                TryReceiveFlash(x - 1, y + 1, width, height, octos);
                TryReceiveFlash(x, y - 1, width, height, octos);
                TryReceiveFlash(x, y + 1, width, height, octos);
                TryReceiveFlash(x + 1, y - 1, width, height, octos);
                TryReceiveFlash(x + 1, y, width, height, octos);
                TryReceiveFlash(x + 1, y + 1, width, height, octos);
            }

            var flashingOctos = octos.SelectMany(row => row).Where(o => o.Flashing).Count();
            octos.SelectMany(row => row).ForEach(o => o.Step());

            return flashingOctos;
        }

        public static void TryReceiveFlash(int x, int y, int width, int height, Octopus[][] octos)
        {
            if (x < 0 || y < 0 || x >= width || y >= height) return;
            octos[y][x].ReceiveFlash();
        }
    }
}