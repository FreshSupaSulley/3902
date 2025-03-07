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

        public BatRoom(Game game, Player player) : base(game, player, data, 
        DoorType.LOCK, DoorType.OPEN, DoorType.LOCK, DoorType.OPEN, 
        "start", "water")
        {
            gameObjects.Add(new Bat(new Vector2(200, 50)));
            gameObjects.Add(new Bat(new Vector2(200, 100)));
        }

        public override void Update(Game game)
        {
            base.Update(game);
        }
        
    }
}
