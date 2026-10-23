namespace CCC_2024_04.Models;

internal static class OutputUtils
{
    public static string ListToString(IEnumerable<string> elements)
    {
        return string.Join(Environment.NewLine, elements);
    }
}