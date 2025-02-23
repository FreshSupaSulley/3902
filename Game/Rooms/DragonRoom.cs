using Game.Entities;
using Game.Tiles;
using Microsoft.Xna.Framework;
using static Game.Tiles.TileType;

namespace Game.Rooms
{
    public class DragonRoom : Room
    {
        private static readonly TileType[] data = [
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, WALL, BLOCK, BLOCK, WALL, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK
        ];

        public DragonRoom(Player player) : base(player, data)
        {
            gameObjects.Add(new Dragon(new Vector2(200, 50)));
        }
    }
}
