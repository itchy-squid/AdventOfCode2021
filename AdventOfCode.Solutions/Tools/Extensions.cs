namespace AdventOfCode.Solutions.Tools
{
    internal static class Extensions
    {
        public static string[] SplitAndClean(this string input, char character)
        {
            return input.SplitAndClean(new[] { character });
        }

        public static string[] SplitAndClean(this string input, char[]? character = null)
        {
            if(character == null)
            {
                character = new[] { ' ', '\r', '\n' };
            }

            return input.Split(character, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        } 
    }
}
