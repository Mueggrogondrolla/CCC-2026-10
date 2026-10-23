using CCCLibrary.Serialization;

namespace CCC_2026_10;

internal class Levels
{
    private InputLexer lineLexer = new InputLexer(['\n', '\r']);

    public string SolveLevel1(string input)
    {
        var fileLines = lineLexer.GetTokens(input);
        var radiiCount = int.Parse(fileLines.Take(1).Single());
        var listOfRadii = InputUtils.ReadRadiiList(fileLines.Skip(1));
    }

    public string SolveLevel2(string input)
    {
        throw new NotImplementedException();
    }

    public string SolveLevel3(string input)
    {
        throw new NotImplementedException();
    }

    public string SolveLevel4(string input)
    {
        throw new NotImplementedException();
    }

    public string SolveLevel5(string input)
    {
        throw new NotImplementedException();
    }

    public string SolveLevel6(string input)
    {
        throw new NotImplementedException();
    }

    public string SolveLevel7(string input)
    {
        throw new NotImplementedException();
    }
}