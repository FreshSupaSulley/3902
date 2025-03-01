using System.Collections.Generic;
using System.Linq;
using Game.Entities;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Rooms
{
    public abstract class Room
    {
        // Every room has the same outline (for now)
        private static readonly Texture2D OUTLINE = Game.Load("Tiles/map_outline.png");

        // Every room in Zelda is 12x7
        public readonly TileType[] tiles;
        public readonly Door topDoor, rightDoor, bottomDoor, leftDoor;
        public readonly List<Entity> gameObjects = [];

        public Room(Player player, TileType[] tiles, DoorType topDoor, DoorType rightDoor, DoorType bottomDoor, DoorType leftDoor)
        {
            gameObjects.Add(player);
            this.tiles = tiles;
            this.topDoor = new Door(topDoor);
            this.rightDoor = new Door(rightDoor);
            this.bottomDoor = new Door(bottomDoor);
            this.leftDoor = new Door(leftDoor);
        }

        /// Called when a door is touched by the player
        public abstract void DoorInteracted(Game game, int direction);

        public virtual void Update(Game game)
        {
            // Not doing any fancy lambda because that would be concurrent modification
            // This apparently works??
            foreach (var entity in gameObjects.ToList())
            {
                entity.Update(game);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            // Draw room outline
            batch.Draw(OUTLINE, new Vector2(0, 0), Color.White);

            for (int i = 0; i < tiles.Length; i++)
            {
                Tile.Draw(tiles[i], new Vector2(Tile.TILE_SIZE * 2 + i % 12 * Tile.TILE_SIZE, Tile.TILE_SIZE * 2 + i / 12 * Tile.TILE_SIZE), batch);
            }

            // Draw doors with rotation
            topDoor.Draw(batch, 0, new Vector2(112, 0));
            rightDoor.Draw(batch, 90, new Vector2(224, 72));
            bottomDoor.Draw(batch, 180, new Vector2(112, 144));
            leftDoor.Draw(batch, 270, new Vector2(0, 72));

            // Draw entities over tiles
            gameObjects.ForEach(entity => entity.Draw(batch));
        }

        public void AddEntity(Entity entity) => gameObjects.Add(entity);
        public void RemoveEntity(Entity entity) => gameObjects.Remove(entity);
    }
}
