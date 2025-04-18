using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.State;
using System.Xml;

namespace Game.Rooms
{
    public class Door
    {
        private static readonly Texture2D DOOR_SHEET_TOP = Main.Load("Tiles/doors.png");
        private static readonly int DOOR_TEXTURE_SIZE = 32;
        private static readonly Vector2 spriteOrigin = new(DOOR_TEXTURE_SIZE / 2, DOOR_TEXTURE_SIZE / 2);

        // Contains all mappings from DoorType to textures
        // This could alternatively be a dictionary to Texture2D? Would be less efficient on memory
        private static readonly Dictionary<DoorType, Rectangle> textures = [];

        // Serialize the following
        public Vector2 Position;
        public int Angle;

        // Serialize these elements
        public DoorType Type;
        public int location;
        public string roomPath;

        /// This is so ugly, but alas, this is a consequence of our XML loading architecture. Can clean up later
        protected internal void Initialize()
        {
            switch (location)
            {
                case 0:
                    Position = new Vector2(112, 0);
                    Angle = 0;
                    break;
                case 1:
                    Position = new Vector2(224, 72);
                    Angle = 90;
                    break;
                case 2:
                    Position = new Vector2(112, 144);
                    Angle = 180;
                    break;
                case 3:
                    Position = new Vector2(0, 72);
                    Angle = 270;
                    break;
            }
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

        public static bool IsWalkable(DoorType door) => (int)door < 32;

        /// Subclasses can inherit Update for special behavior
        public virtual void Update(State.Game game)
        {
            if (new Rectangle((int)Position.X - (int)game.player.Position.X, (int)Position.Y - (int)game.player.Position.Y, DOOR_TEXTURE_SIZE, DOOR_TEXTURE_SIZE).Intersects(game.player.collisionBox))
            {
                int oldKeys = game.player.GetKey();
                game.SwitchRoom((location + 2) % 4, Room.LoadRoom(roomPath, game.player));
                game.player.setKey(oldKeys);
                if (Type == DoorType.LOCK && game.player.GetKey() >= 1)
                {
                    game.player.useKey();
                    Type = DoorType.OPEN;
                }
            }
            
        }


        /// Draws the door
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DOOR_SHEET_TOP, Position + spriteOrigin, textures[Type], Color.White, (float)(Angle * Math.PI / 180f), spriteOrigin, 1.0f, SpriteEffects.None, 0);
            if (Main.debug) {
                DebugTools.DrawRect(spriteBatch, new Rectangle((int)Position.X, (int)Position.Y, DOOR_TEXTURE_SIZE, DOOR_TEXTURE_SIZE), new Color(Color.Blue, 0.5f));
            }
        }
    }
}
