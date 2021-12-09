namespace AdventOfCode.Solutions.Tools
{
    internal static class StringExtensions
    {
        public static string[] SplitLines(this string input)
        {
            return input.SplitAndClean(new[] { '\n' });
        }

        public static string[] Tokenize(this string input)
        {
            return input.SplitAndClean(new[] { ' ', '\n' });
        }

        public static string[] SplitAndClean(this string input, char[] character)
        {
            return input.Split(character, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        } 
    }
}
