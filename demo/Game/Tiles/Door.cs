using System;
using System.Collections.Generic;
using Game.Entities;
using Game.Items;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Tiles
{
    public class Door(DoorType type) : IGameObject
    {
        //private static readonly Texture2D DOOR_SHEET_TOP = Game.Load("Tiles/doors_top.png");
        //private static readonly Texture2D DOOR_SHEET_LEFT = Game.Load("Tiles/doors_left.png");
        //private static readonly Texture2D DOOR_SHEET_RIGHT = Game.Load("Tiles/doors_right.png");
        private static readonly Texture2D DOOR_SHEET_BOTTOM = Game.Load("Tiles/doors_bottom.png");
        private static readonly int DOOR_TEXTURE_SIZE = 32;

        // Contains all mappings from DoorType to textures
        // This could alternatively be a dictionary to Texture2D? Would be less efficient on memory
        private static readonly Dictionary<DoorType, Rectangle> textures = [];

        public DoorType Type { get; set; } = type;

        // A single tile can contain ONE item on it (for now)
        public Item Item { get; set; }

        public static void LoadTextures()
        {
            int DOORS_PER_ROW = DOOR_SHEET_BOTTOM.Width / DOOR_TEXTURE_SIZE;
            foreach (DoorType item in Enum.GetValues(typeof(DoorType)))
            {
                int ordinal = (int)item;
                textures.Add(item, new Rectangle(ordinal % DOORS_PER_ROW * DOOR_TEXTURE_SIZE, ordinal / DOORS_PER_ROW * DOOR_TEXTURE_SIZE, DOOR_TEXTURE_SIZE, DOOR_TEXTURE_SIZE));
            }
        }


        /// Subclasses can inherit Update for special behavior
        public virtual void Update() { }

        /// Draws the door
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(DOOR_SHEET_TOP, new System.Numerics.Vector2(300, 300), textures[Type], Color.White);
            //spriteBatch.Draw(DOOR_SHEET_LEFT, new System.Numerics.Vector2(200, 350), textures[Type], Color.White);
            //spriteBatch.Draw(DOOR_SHEET_RIGHT, new System.Numerics.Vector2(400, 350), textures[Type], Color.White);
            spriteBatch.Draw(DOOR_SHEET_BOTTOM, new System.Numerics.Vector2(200, 400), textures[Type], Color.White);
            // Fun c# note: "is" can't be overriden but == can apparently
            if (Item is not null)
            {
                // TODO: somehow pass position down
                Item.Draw(spriteBatch);
            }
        }
    }
}
