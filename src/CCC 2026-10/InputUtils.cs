namespace CCC_2026_10;

public static class InputUtils
{
    public static List<int> ReadRadiiList(IEnumerable<string> lines)
    {
        return lines.Select(line => int.Parse((line))).ToList();
    }
}