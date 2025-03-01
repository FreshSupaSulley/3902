namespace Game.Tiles
{
    public enum TileType
    {
        // You can walk and perhaps griddy on these tiles if so desired
        BLOCK, SAND, STAIRS, BLACK, BRIDGE,

        // Walls have ordinals after this will be incremented by 1
        WALL = 128, BRICK, FISH, WATER
    }
}
