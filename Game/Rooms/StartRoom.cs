using System.Xml.Serialization;
using Game.Entities;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static Game.Tiles.TileType;

namespace Game.Rooms
{
    public class StartRoom : Room
    {
        [XmlElement(Type = typeof(TileType))]
        public static TileType[] data = [
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK
        ];

        public StartRoom(Player player) : base(player, data, DoorType.BREAK, DoorType.WALL, DoorType.OPEN, DoorType.PUZZLE)
        {
            
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
                game.SwitchRoom(direction, new BatRoom(game.player));
            }
        }
    }
}
