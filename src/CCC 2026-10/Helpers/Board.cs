
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
            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                if (x % 2 == 0)
                {
                    stringBuilder.Append('-');
                }
                stringBuilder.Append(string.Join('-', Tiles[x, y].AsCharacter()));
            }
            stringBuilder.AppendLine();
        }
        return stringBuilder.ToString();
    }
}