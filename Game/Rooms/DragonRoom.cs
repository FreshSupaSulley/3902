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

        public DragonRoom(Game game, Player player) : base(game, player, data, 
        DoorType.BREAK, DoorType.WALL, DoorType.OPEN, DoorType.PUZZLE, 
        "water", "start")
        {
            gameObjects.Add(new Dragon(new Vector2(200, 50)));
        }

        public override void Update(Game game)
        {
            // Temp behavior
            base.Update(game);
        }

    }
}
