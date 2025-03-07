using Game.Entities;
using Game.Tiles;
using Microsoft.Xna.Framework.Input;
using static Game.Tiles.TileType;

namespace Game.Rooms
{
    public class WaterRoom : Room
    {
        private static readonly TileType[] data = [
            WATER, WATER, WATER, WATER, WATER, BLOCK, BLOCK, WATER, WATER, WATER, WATER, WATER,
            WATER, BLOCK, BLOCK, BLOCK, WATER, BLOCK, BLOCK, BLOCK, BLOCK, WATER, BLOCK, WATER,
            WATER, BLOCK, WATER, BLOCK, WATER, BLOCK, WATER, WATER, BLOCK, WATER, BLOCK, WATER,
            BLOCK, BLOCK, WATER, BLOCK, WATER, BLOCK, BLOCK, WATER, BLOCK, WATER, BLOCK, BLOCK,
            WATER, BLOCK, WATER, BLOCK, WATER, WATER, BLOCK, WATER, BLOCK, WATER, BLOCK, WATER,
            WATER, BLOCK, WATER, BLOCK, BLOCK, BLOCK, BLOCK, WATER, BLOCK, BLOCK, BLOCK, WATER,
            WATER, WATER, WATER, WATER, WATER, BLOCK, BLOCK, WATER, WATER, WATER, WATER, WATER
        ];

        public WaterRoom(Game game, Player player) : base(game, player, data, 
        DoorType.LOCK, DoorType.OPEN, DoorType.LOCK, DoorType.OPEN, 
        "bat", "dragon")
        {
            // gameObjects.Add(new Dragon(new Vector2(200, 50)));
        }

        public override void Update(Game game)
        {
            base.Update(game);
        }
        
    }
}
