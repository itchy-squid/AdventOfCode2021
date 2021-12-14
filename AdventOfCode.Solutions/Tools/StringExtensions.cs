namespace AdventOfCode.Solutions.Tools
{
    public static class StringExtensions
    {
        public static string[] SplitLines(this string input)
        {
            return input.Split(new[] { '\n' }, StringSplitOptions.TrimEntries);
        }

        public static string[] Tokenize(this string input)
        {
            return input.SplitAndClean(new[] { ' ', '\n' });
        }

        public static string[] SplitAndClean(this string input, char[] character)
        {
            return input.Split(character, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        } 

        public static int ToInt(this char c)
        {
            return c - '0';
        }
    }
}
