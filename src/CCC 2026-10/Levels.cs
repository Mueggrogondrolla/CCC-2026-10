using System.Data.Common;
using System.Text;
using CCCLibrary.Serialization;

namespace CCC_2026_10;

internal class Levels
{
    private readonly InputLexer _lineLexer = new InputLexer(['\n', '\r']);
    
    private const char IndentCharacter = '-';
    private const char EdgeCharacter = '*';
    private const char BoardCharacter = 'O';

    public string SolveLevel1(string input)
    {
        var fileLines = _lineLexer.GetTokens(input);
        var listOfRadii = InputUtils.ReadRadiiList(fileLines.Skip(1));

        var boards = new List<IEnumerable<string>>();
        foreach (var radius in listOfRadii)
        {
            var boardWidth = radius * 2 - 1;
            var boardHeight = radius * 2 - 1;
            var totalWidth = boardWidth * 2 + 1 + 2;
            var totalHeight = boardHeight + 2;
            var edgeLength = radius + 1;
            boards.Add(GenerateBoard(totalHeight, totalWidth, edgeLength));
        }

        var serializedBoards = new List<string>();
        foreach (var board in boards)
        {
            
        }
        return string.Join(Environment.NewLine + Environment.NewLine, boards.Select(board => string.Join(Environment.NewLine, board)));
    }
    
    IEnumerable<string> GenerateBoard(int totalHeight, int totalWidth, int edgeLength)
    {
        var boardLength = edgeLength;
            
        yield return GenerateTopOrBottomLine(totalWidth, edgeLength);
            
        for (int i = 0; i < totalHeight / 2; i++)
        {
            yield return GenerateMiddleLine(totalWidth, ++boardLength);
        }
            
        for (int i = 0; i < totalHeight / 2 - 1; i++)
        {
            yield return GenerateMiddleLine(totalWidth, --boardLength);
        }
            
        yield return GenerateTopOrBottomLine(totalWidth, edgeLength);
    }

    string GenerateTopOrBottomLine(int totalWidth, int boardLength)
    {
        var indentation = (totalWidth - (boardLength * 2 - 1)) / 2;
            
        return string.Join("", Enumerable.Repeat(IndentCharacter, indentation)) +
               string.Join(IndentCharacter, Enumerable.Repeat(EdgeCharacter, boardLength)) +
               string.Join("", Enumerable.Repeat(IndentCharacter, indentation));
    }

    string GenerateMiddleLine(int totalWidth, int boardLength)
    {
        var indentation = (totalWidth - (boardLength * 2 - 1)) / 2;
        var innerBoard = string.Join(IndentCharacter, Enumerable.Repeat(BoardCharacter, boardLength - 2));
            
        return  string.Join("", Enumerable.Repeat(IndentCharacter, indentation)) +
               EdgeCharacter + IndentCharacter +
               innerBoard + 
               IndentCharacter + EdgeCharacter +
               string.Join("", Enumerable.Repeat(IndentCharacter, indentation));
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