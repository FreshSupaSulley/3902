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

        public WaterRoom(Player player) : base(player, data, DoorType.LOCK, DoorType.OPEN, DoorType.LOCK, DoorType.OPEN)
        {
            // gameObjects.Add(new Dragon(new Vector2(200, 50)));
        }

        public override void Update(Game game)
        {
            // Temp behavior
            base.Update(game);
            if (game.keyboard.IsKeyPressed(Keys.R))
            {
                DoorInteracted(game, 1);
            }
        }
        
        public override void DoorInteracted(Game game, int direction)
        {
            if (direction == 1)
            {
                game.SwitchRoom(direction, new DragonRoom(game.player));
            }
        }
    }
}
