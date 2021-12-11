namespace AdventOfCode.Solutions.Day11
{
    public class Octopus
    {
        public int X { get; init; }
        public int Y { get; init; }
        public int Lumens { get; private set; }
        public bool Flashing { get; private set; }
        
        public Octopus(int x, int y, int lumens)
        {
            X = x;
            Y = y;
            Lumens = lumens;
        }

        public bool WillFlash()
        {
            return Lumens >= 9 && !Flashing;
        }

        public void Flash()
        {
            Flashing = true;
        }

        public void ReceiveFlash()
        {
            Lumens++;
        }

        public void Step()
        {
            Lumens = (Lumens >= 9) ? 0 : Lumens + 1;
            Flashing = false;
        }
    }
}
