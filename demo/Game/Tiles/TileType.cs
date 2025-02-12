using System;

namespace Game.Tiles
{
    public enum TileType
    {
        // You can walk and perhaps griddy on these things if so desired
        BLOCK, FISH, SAND, EMPTY, STAIRS, BLACK, BRIDGE,

        // Walls have ordinals after this will be incremented by 1
        WALL = 64, BRICK
    }
}
