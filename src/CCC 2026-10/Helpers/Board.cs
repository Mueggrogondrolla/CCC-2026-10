
using System.Text;

public class Board
{
    public TileType[,] Tiles { get; set; }

    public Board(int width, int height)
    {
        Tiles = new TileType[width, height];
    }

    public string Render()
    {
        var stringBuilder = new StringBuilder();
        for (int y = 0; y < Tiles.GetLength(1); y++)
        {
            var lineBuilder = new StringBuilder();
            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                lineBuilder.Append(Tiles[x, y].AsCharacter());
            }

            string lineContent = string.Join('-', lineBuilder.ToString().ToCharArray());
            if (y % 2 == 0)
            {
                lineContent = "-" + lineContent.Substring(0, lineContent.Length - 1);
            }
            stringBuilder.AppendLine(lineContent);
        }
        return stringBuilder.ToString();
    }
}