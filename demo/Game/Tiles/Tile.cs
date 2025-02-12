using System;
using System.Collections.Generic;
using Game.Entities;
using Game.Items;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Tiles
{
    public class Tile(TileType type) : IGameObject
    {
        private static readonly Texture2D TILES = Game.Load("Tiles/tiles.png");
        private static readonly int TILE_SIZE = 16;

        // Contains all mappings from TileType to textures
        // This could alternatively be a dictionary to Texture2D? Would be less efficient on memory
        private static readonly Dictionary<TileType, Rectangle> textures = [];

        public TileType type = type;

        // A single tile can contain ONE item on it (for now)
        public Item item { get; set; }

        public static void LoadTextures()
        {
            int TILES_PER_ROW = TILES.Width / TILE_SIZE;
            foreach (TileType item in Enum.GetValues(typeof(TileType)))
            {
                int ordinal = (int) item;
                textures.Add(item, new Rectangle(ordinal % TILES_PER_ROW * TILE_SIZE, ordinal / TILES_PER_ROW * TILE_SIZE, TILE_SIZE, TILE_SIZE));
            }
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TILES, new System.Numerics.Vector2(200, 200), textures[type], Color.White);
        }
    }
}
