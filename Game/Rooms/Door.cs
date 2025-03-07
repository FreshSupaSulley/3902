using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Collision;
using Game.Entities;
using Game.Commands;

namespace Game.Rooms
{
    public class Door
    {
        private static readonly Texture2D DOOR_SHEET_TOP = Game.Load("Tiles/doors.png");
        private static readonly int DOOR_TEXTURE_SIZE = 32;
        private static readonly Vector2 spriteOrigin = new(DOOR_TEXTURE_SIZE / 2, DOOR_TEXTURE_SIZE / 2);

        // Contains all mappings from DoorType to textures
        // This could alternatively be a dictionary to Texture2D? Would be less efficient on memory
        private static readonly Dictionary<DoorType, Rectangle> textures = [];

        public DoorType Type { get; set; }
        public CollisionBox collisionBox;

        public Door(DoorType type, int doorAlignment, Room destinationRoom, Game game) {
            Type = type;
            ICommand command = new ChangeRoomCommand(doorAlignment, destinationRoom, game);
            Rectangle bounds = new Rectangle();
            switch (doorAlignment) {
                case 0:
                    bounds = new Rectangle(64,0,16,8);
                    break;
                case 1:
                    bounds = new Rectangle(0,64,8,16);
                    break;
                case 2: // top
                    // bounds = new Rectangle(,136,16,8);
                    break;
                case 3: // bottom
                    // bounds = new Rectangle(216,64,8,16);
                    break;
            }

            collisionBox = new DoorCollisionBox(bounds, command);
        }

        public static void LoadTextures()
        {
            int DOORS_PER_ROW = DOOR_SHEET_TOP.Width / DOOR_TEXTURE_SIZE;
            foreach (DoorType item in Enum.GetValues(typeof(DoorType)))
            {
                int ordinal = (int)item;
                textures.Add(item, new Rectangle(ordinal % DOORS_PER_ROW * DOOR_TEXTURE_SIZE, ordinal / DOORS_PER_ROW * DOOR_TEXTURE_SIZE, DOOR_TEXTURE_SIZE, DOOR_TEXTURE_SIZE));
            }
        }

        public static bool IsWalkable(DoorType door) => (int) door < 32;

        /// Subclasses can inherit Update for special behavior
        public virtual void Update() {
            collisionBox.Update();
        }

        /// Draws the door
        public void Draw(SpriteBatch spriteBatch, int angle, Vector2 position) => spriteBatch.Draw(DOOR_SHEET_TOP, position + spriteOrigin, textures[Type], Color.White, (float) (angle * Math.PI / 180f), spriteOrigin, 1.0f, SpriteEffects.None, 0);
    }
}
