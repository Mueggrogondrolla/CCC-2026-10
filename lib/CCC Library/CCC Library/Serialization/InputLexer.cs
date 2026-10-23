namespace CCCLibrary.Serialization;

public sealed class InputLexer
{
    public char[] Separators { get; init; }

    public InputLexer(char[] separators)
    {
        Separators = separators;
    }

    public IEnumerable<string> GetTokens(string input)
    {
        return input.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
    }
}
