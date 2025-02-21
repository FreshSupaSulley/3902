using System;

namespace Game.Tiles
{
    public enum DOORTYPE
    {
        // You can walk and perhaps griddy on these tiles if so desired
        WALL, OPEN, LOCK, PUZZLE, BREAK,

        // Walls have ordinals after this will be incremented by 1
        WALL = 128, BRICK, FISH
    }
}
