using AdventOfCode.Solutions.Tools;

namespace AdventOfCode.Solutions.Day11
{
    public static class Program
    {
        public static void Main()
        {
            var input = Input.ReadAllLines("Day11");

            var result = Solve(input, 100);
            Console.WriteLine($"No. of flashes: {result}");
            Console.WriteLine();
        }

        public static int Solve(IEnumerable<string> input, int steps)
        {
            var octopuses = input
                .Select((row, y) => 
                    row.Select((octopus, x) => new Octopus(x, y, octopus.ToInt())).ToArray())
                .ToArray();

            return CountFlashes(octopuses, steps);
        }

        public static int CountFlashes(Octopus[][] input, int steps)
        {
            return Enumerable.Range(0, steps).Select(i => Step(input)).Sum();
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