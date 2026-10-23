public static class BoardBuilder
{
    public static Board BuildBoardWithRadius(int radius)
    {
        int width = radius * 2 + 1;
        int height = radius * 2 + 1;
        var board = new Board(width, height);

        TileType[,] tiles = new TileType[width, height];

        int edgeLength = radius + 1;

        for (int i = 0; i < edgeLength; i++)
        {
            tiles[i + ((width - edgeLength) / 2), 0] = TileType.Edge;
        }
        
        for (int y = 1; y < (height / 2) + 1; y++)
        {
            int currentWidth = width;
            if (y % 2 == 0)
            {
                currentWidth -= 1; // Reduce width for odd rows
            }
            int offset = ((currentWidth - edgeLength - y) / 2);

            // set edge tiles
            tiles[offset, y] = TileType.Edge;
            tiles[currentWidth - offset - 1, y] = TileType.Edge;

            for (int x = offset + 1; x < currentWidth - offset - 1; x++)
            {
                tiles[x, y] = TileType.Board;
            }
        }

        // Mirror the tiles vertically to create the bottom half of the board
        var mirroredTiles = MirrorTilesVertically(tiles);

        board.Tiles = mirroredTiles;

        return board;
    }

    private static TileType[,] MirrorTilesVertically(TileType[,] tiles)
    {
        int width = tiles.GetLength(0);
        int height = tiles.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                tiles[x, height - 1 - y] = tiles[x, y];
            }
        }

        return tiles;
    }
}