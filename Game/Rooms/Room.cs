using System.Collections.Generic;
using System.Linq;
using Game.Entities;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Rooms
{
    public class Room
    {
        // Every room has the same outline (for now). Could change if 
        private static readonly Texture2D OUTLINE = Game.Load("Tiles/map_outline.png");

        // Every room in Zelda is 12x7
        public readonly TileType[] tiles;
        public readonly List<Entity> gameObjects = [];

        public Room(Player player, TileType[] tiles)
        {
            gameObjects.Add(player);
            this.tiles = tiles;
        }

        public void Update(Game game)
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

            // Draw entities over tiles
            gameObjects.ForEach(entity => entity.Draw(batch));
        }

        public void AddEntity(Entity entity) => gameObjects.Add(entity);
        public void RemoveEntity(Entity entity) => gameObjects.Remove(entity);
    }
}
