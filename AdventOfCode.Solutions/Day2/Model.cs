using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Day2
{
    public class Instruction
    {
        public int Forward { get; init; }
        public int Up { get; init; }
        public int Down { get; init; }

        public static Instruction CreateFrom(Match match)
        {
            return new Instruction()
            {
                Forward = ParseOrDefault(match.Groups["forwardNo"].Value),
                Down = ParseOrDefault(match.Groups["downNo"].Value),
                Up = ParseOrDefault(match.Groups["upNo"].Value)
            };
        }

        public static int ParseOrDefault(string input)
        {
            return !string.IsNullOrEmpty(input) ? int.Parse(input) : 0;
        }
    }
    
    public abstract class Submarine
    {
        public int Horizontal { get; protected set; }
        public int Depth { get; protected set; }
        public int Aim { get; protected set; }

        public abstract void Move(Instruction instruction);
    }

    public class Problem1Submarine : Submarine
    {
        public override void Move(Instruction inst)
        {
            Horizontal += inst.Forward;
            Depth += inst.Down - inst.Up;
        }
    }

    public class Problem2Submarine : Submarine
    {
        public override void Move(Instruction inst)
        {
            Horizontal += inst.Forward;
            Depth += Aim * inst.Forward;
            Aim += inst.Down - inst.Up;
        }
    }
}
