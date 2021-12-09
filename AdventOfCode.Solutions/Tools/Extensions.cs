namespace AdventOfCode.Solutions.Tools
{
    internal static class Extensions
    {
        public static string[] SplitAndClean(this string input, char character = ' ')
        {
            return input.Split(character, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        } 
    }
}
