using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.State;
using System.Xml;
using Game.Entities;

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
        public DoorType Type;
        public int location;
        public string roomPath;

        // Don't serialize
        private Room room;
        private int[] tileNums;

        /// This is so ugly, but alas, this is a consequence of our XML loading architecture. Can clean up later
        protected internal void Initialize(string filename, Room room)
        {
            if (string.IsNullOrEmpty(roomPath) && Type != DoorType.WALL)
            {
                throw new Exception("Room path isn't set for this door at room " + filename);
            }
            this.room = room;
            switch (location)
            {
                case 0:
                    Position = new Vector2(112, 0);
                    Angle = 0;
                    tileNums = [6, 7];
                    break;
                case 1:
                    Position = new Vector2(224, 72);
                    Angle = 90;
                    tileNums = [69];
                    break;
                case 2:
                    Position = new Vector2(112, 144);
                    Angle = 180;
                    tileNums = [118, 119];
                    break;
                case 3:
                    Position = new Vector2(0, 72);
                    Angle = 270;
                    tileNums = [56];
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
        public virtual bool Update(State.Game game)
        {
            if (Type == DoorType.PUZZLE)
            {
                if (room.gameObjects.Count <= 1)
                {
                    Type = DoorType.OPEN;
                    foreach (int tileNum in tileNums)
                    {
                        room.tiles[tileNum] = Tiles.TileType.BLOCK;
                    }
                }
            }
            else if (Type == DoorType.LOCK)
            {
                foreach (Player player in game.players)
                {
                    if (player.HasKey())
                    {
                        // Unlock the door
                        Type = DoorType.OPEN;
                        foreach (int tileNum in tileNums)
                        {
                            room.tiles[tileNum] = Tiles.TileType.BLOCK;
                        }
                    }
                }
            }
            bool anyKey = false;
            foreach (Player player in game.players)
            {
                // If we're inside the door activation box, which you should only be able to do if the door is actually interactable
                if (new Rectangle((int)Position.X - (int)player.Position.X, (int)Position.Y - (int)player.Position.Y, DOOR_TEXTURE_SIZE, DOOR_TEXTURE_SIZE).Intersects(player.collisionBox))
                {
                    int oldKeys = player.GetKey();
                    game.SwitchRoom((location + 2) % 4, Room.LoadRoom(roomPath, game.players));
                    player.setKey(oldKeys);

                    if (player.HasKey())
                    {
                        anyKey = true;
                    }
                    if (Type == DoorType.LOCK && anyKey)
                    {
                        player.useKey();
                        Type = DoorType.OPEN;
                    }
                    // I assume we don't need to run this for each player
                    return true;
                }
            }
            return false;
        }

        /// Draws the door
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DOOR_SHEET_TOP, Position + spriteOrigin, textures[Type], Color.White, (float)(Angle * Math.PI / 180f), spriteOrigin, 1.0f, SpriteEffects.None, 0);
            if (Main.debug)
            {
                DebugTools.DrawRect(spriteBatch, new Rectangle((int)Position.X, (int)Position.Y, DOOR_TEXTURE_SIZE, DOOR_TEXTURE_SIZE), new Color(Color.Blue, 0.5f));
            }
        }
    }
}
