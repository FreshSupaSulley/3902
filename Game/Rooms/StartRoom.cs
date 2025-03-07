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

        public StartRoom(Game game, Player player) : base(game, player, data, DoorType.BREAK, DoorType.WALL, DoorType.OPEN, DoorType.PUZZLE, new DragonRoom(game, game.player), new BatRoom(game, game.player))
        {
            
        }

        public override void Update(Game game)
        {
            base.Update(game);
        }

    }
}
