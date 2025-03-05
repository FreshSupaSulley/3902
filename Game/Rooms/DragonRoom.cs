using System.Xml.Serialization;
using Game.Entities;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static Game.Tiles.TileType;

namespace Game.Rooms
{
    [XmlType("Dragon room")]
    public class DragonRoom : Room
    {
        [XmlElement(Type = typeof(TileType))]
        public static TileType[] data = [
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, WALL, BLOCK, BLOCK, WALL, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK,
            BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK, BLOCK
        ];

        public DragonRoom(Player player) : base(player, data, DoorType.BREAK, DoorType.WALL, DoorType.OPEN, DoorType.PUZZLE)
        {
            gameObjects.Add(new Dragon(new Vector2(200, 50)));
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
                game.SwitchRoom(direction, new WaterRoom(game.player));
            }
        }
    }
}
