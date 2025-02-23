using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Tiles
{
    public static class Tile
    {
        private static readonly Texture2D TILE_SHEET = Game.Load("Tiles/tiles.png");

        // Size of a tile (width and height) in pixels
        public static readonly int TILE_SIZE = 16;

        // Contains all mappings from TileType to textures
        // This could alternatively be a dictionary to Texture2D? Would be less efficient on memory
        private static readonly Dictionary<TileType, Rectangle> textures = [];

        public static void LoadTextures()
        {
            int TILES_PER_ROW = TILE_SHEET.Width / TILE_SIZE;
            foreach (TileType item in Enum.GetValues(typeof(TileType)))
            {
                int ordinal = (int)item;
                textures.Add(item, new Rectangle(ordinal % TILES_PER_ROW * TILE_SIZE, ordinal / TILES_PER_ROW * TILE_SIZE, TILE_SIZE, TILE_SIZE));
            }
        }

        // true if this tile is walkable, false otherwise (it's a wall)
        public static bool IsWalkable(TileType tile) => (int) tile < 128;

        // Draws the tile
        public static void Draw(TileType tile, Vector2 position, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TILE_SHEET, position, textures[tile], Color.White);
        }
    }
}
