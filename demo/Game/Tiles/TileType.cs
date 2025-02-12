using System;

namespace Game.Tiles
{
    public enum TileType
    {
        // You can walk and perhaps griddy on these tiles if so desired
        BLOCK, FISH, SAND, EMPTY, STAIRS, BLACK, BRIDGE,

        // Walls have ordinals after this will be incremented by 1
        WALL = 128, BRICK
    }
}
