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
        private static readonly Texture2D TILE_SHEET = Game.Load("Tiles/tiles.png");
        private static readonly int TILE_TEXTURE_SIZE = 16;

        // Contains all mappings from TileType to textures
        // This could alternatively be a dictionary to Texture2D? Would be less efficient on memory
        private static readonly Dictionary<TileType, Rectangle> textures = [];

        public TileType Type { get; set; } = type;

        // A single tile can contain ONE item on it (for now)
        public Item Item { get; set; }

        public static void LoadTextures()
        {
            int TILES_PER_ROW = TILE_SHEET.Width / TILE_TEXTURE_SIZE;
            foreach (TileType item in Enum.GetValues(typeof(TileType)))
            {
                int ordinal = (int)item;
                textures.Add(item, new Rectangle(ordinal % TILES_PER_ROW * TILE_TEXTURE_SIZE, ordinal / TILES_PER_ROW * TILE_TEXTURE_SIZE, TILE_TEXTURE_SIZE, TILE_TEXTURE_SIZE));
            }
        }

        /// true if this tile is walkable, false otherwise (it's a wall)
        public bool IsWalkable() => ((int)Type) < 128;

        /// Subclasses can inherit Update for special behavior
        public virtual void Update() { }

        /// Draws the tile
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TILE_SHEET, new System.Numerics.Vector2(200, 200), textures[Type], Color.White);
            // Fun c# note: "is" can't be overriden but == can apparently
            if (Item is not null)
            {
                // TODO: somehow pass position down
                Item.Draw(spriteBatch);
            }
        }
    }
}
