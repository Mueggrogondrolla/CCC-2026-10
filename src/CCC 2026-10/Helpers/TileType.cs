

public enum TileType
{
    Empty,
    Edge,
    Board,
}

public static class TileTypeExtensions
{
    public static char AsCharacter(this TileType tileType)
    {
        return tileType switch
        {
            TileType.Empty => ' ',
            TileType.Edge => '*',
            TileType.Board => 'O',
            _ => throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null)
        };
    }
}