using CCCLibrary.Serialization;

namespace CCC_2026_10;

internal class Levels
{
    private InputLexer lineLexer = new InputLexer(['\n', '\r']);

    public string SolveLevel1(string input)
    {
        var lines = input.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries);

        string totalOutput = "";

        for (int i = 0; i < lines.Length; i++)
        {
            var board = BoardBuilder.BuildBoardWithRadius(int.Parse(lines[i]));
            totalOutput += board.Render();
            totalOutput += "\n";
        }

        return totalOutput;
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