using System.Xml.Serialization;
using Game.Entities;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static Game.Tiles.TileType;

namespace Game.Rooms
{
    public class BatRoom : Room
    {
        private static readonly TileType[] data = [
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, WALL, WALL, BLOCK, BLOCK, BLOCK, BLOCK, WALL, WALL, BLOCK, BLOCK,
            BLOCK, BLOCK, WALL, WALL, BLOCK, BLOCK, BLOCK, BLOCK, WALL, WALL, BLOCK, BLOCK,
            BLOCK, BLOCK, WALL, WALL, BLOCK, BLOCK, BLOCK, BLOCK, WALL, WALL, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK
        ];

        public BatRoom(Player player) : base(player, data, DoorType.LOCK, DoorType.OPEN, DoorType.LOCK, DoorType.OPEN)
        {
            gameObjects.Add(new Bat(new Vector2(200, 50)));
            gameObjects.Add(new Bat(new Vector2(200, 100)));
        }

        public override void Update(Game game)
        {
            // Temp behavior
            base.Update(game);
            if (game.keyboard.IsKeyPressed(Keys.R))
            {
                DoorInteracted(game, 1);
            }
            if (game.keyboard.IsKeyPressed(Keys.T))
            {
                DoorInteracted(game, 2);
            }
        }
        
        public override void DoorInteracted(Game game, int direction)
        {
            if (direction == 1)
            {
                game.SwitchRoom(direction, new WaterRoom(game.player));
            }
            if (direction == 2)
            {
                game.SwitchRoom(direction, new StartRoom(game.player));
            }
        }
    }
}
