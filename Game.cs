using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Game.Controllers;
using Game.Entities;
using Microsoft.Xna.Framework;
using Game.Tiles;
using Game.Rooms;
using System;
using demo.Game;
using System.Collections.Generic;
using Game.Collision;

namespace Game
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        // Ratio
        public static Vector2 BASE_TO_WINDOW;

        // Used to load resources statically
        private static GraphicsDevice device;
        private GraphicsDeviceManager graphics;
        private RenderTarget2D target, loadingTarget;

        // Used for rendering everything
        private SpriteBatch spriteBatch;
        private SpriteFont font;

        // Visible to inheritors
        public Player player;
        public Room room;
        public KeyboardController keyboard;
        public MouseController mouse;

        // Pertains to loading rooms
        private readonly int LOADING_TIME = 60;
        private int loadingTime;
        private int loadingDirection;
        private Room loadingRoom;
        public Dictionary<String, Room> rooms;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1600 / 2,
                PreferredBackBufferHeight = 1100 / 2
            };
            // Window.AllowUserResizing = true;
            Window.Title = "Bombardier Beetles - Sprint 3";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Size of Zelda map
            target = new RenderTarget2D(graphics.GraphicsDevice, (12 + 4) * 16, (7 + 4) * 16);
            loadingTarget = new RenderTarget2D(graphics.GraphicsDevice, target.Width, target.Height);
            loadingDirection = 5;

            BASE_TO_WINDOW = new Vector2(graphics.PreferredBackBufferWidth / target.Bounds.Width, graphics.PreferredBackBufferHeight / target.Bounds.Height);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Controllers
            keyboard = new KeyboardController(this);
            mouse = new MouseController(this);

            // Room Handling

            rooms = new();

            // This calls load content
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Used to load resources statically
            device = graphics.GraphicsDevice;
            Tile.LoadTextures();
            Door.LoadTextures();
            font = Content.Load<SpriteFont>("Font");
            // Player object that stays around throughout session
            player = new();
            // Add room
            
            rooms.Add("water", new WaterRoom(this, player));
            rooms.Add("bat", new BatRoom(this, player));
            rooms.Add("dragon", new DragonRoom(this, player));
            // rooms.Add("dragon", FileUtils.loadRoom(room, "dragon_room"));    
            rooms.Add("start", new StartRoom(this, player));

            room = rooms["start"];

            foreach (KeyValuePair<String, Room> pair in rooms) {
                pair.Value.PostLoad();
            }

            // Pow
            TempBuffer.pow = Load("pow.png");
            // ControllerLoader.LoadSprint2Commands(tile, this, keyboard, p, entities, gameObjects);
        }

        // Tick
        protected override void Update(GameTime gameTime)
        {
            // Probably unnecessary?
            base.Update(gameTime);

            // Always update inputs first
            keyboard.Update(gameTime);
            mouse.Update(gameTime);

            // Document how much time has passed (probably not how I'm supposed to do it but I'm grasping at straws here :/ - Ty) 
            TempBuffer.elapsed = gameTime.ElapsedGameTime.Milliseconds;
            // Remove any expired temporary entities
            TempBuffer.depreciate(this);

            // Tick room if not mid-transition
            if (loadingRoom is null)
            {
                room.Update(this);
            }
            // If we are switching between rooms
            else
            {
                if (loadingTime++ >= LOADING_TIME)
                {
                    room = loadingRoom;
                    room.PostLoad();
                    loadingRoom = null;
                    loadingTime = 0;
                    // Put player in correct location
                    switch(loadingDirection)
                    {
                        case 0:
                        {
                            player.ActiveAnimation = Player.DOWN;
                            player.Position = new(144 - (player.collisionBox.bounds.Width + player.collisionBox.bounds.X * 2 + 32) / 2, 32 - player.collisionBox.bounds.Y);
                            break;
                        }
                        case 2:
                        {
                            player.ActiveAnimation = Player.LEFT;
                            player.Position = new(220 - (player.collisionBox.bounds.Width - player.collisionBox.bounds.X), 96 - (player.collisionBox.bounds.Y + player.collisionBox.bounds.Height + 32) / 2);
                            break;
                        }
                        case 3:
                        {
                            player.ActiveAnimation = Player.UP;
                            player.Position = new(144 - (player.collisionBox.bounds.X + player.collisionBox.bounds.Width + 32) / 2, 144 - (player.collisionBox.bounds.Y + player.collisionBox.bounds.Height));
                            break;
                        }
                        case 1:
                        {
                            player.ActiveAnimation = Player.RIGHT;
                            player.Position = new(11 + (player.collisionBox.bounds.Width - player.collisionBox.bounds.X), 96 - (player.collisionBox.bounds.Y + player.collisionBox.bounds.Height + 32) / 2);
                            break;
                        }
                    }
                }
            }

            // Always end with post ticks
            keyboard.PostUpdate();
            mouse.PostUpdate();
        }

        // Render
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw room at scaled resolution
            GraphicsDevice.SetRenderTarget(target);
            spriteBatch.Begin();
            room.Draw(spriteBatch);
            spriteBatch.End();

            // If loading
            if (loadingRoom is not null)
            {
                GraphicsDevice.SetRenderTarget(loadingTarget);
                spriteBatch.Begin();
                loadingRoom.Draw(spriteBatch);
                spriteBatch.End();
            }

            // Number of pixels
            int offset = (int) (Math.Pow(Math.Sin(loadingTime * Math.PI / 2 / LOADING_TIME), 2) * Window.ClientBounds.Width);
            int offsetBack = (int)(Math.Pow(Math.Cos(loadingTime * Math.PI / 2 / LOADING_TIME), 2) * Window.ClientBounds.Width);
            // Switch back to main backbuffer and draw buffer
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            switch (loadingDirection)
            {
                case 1:
                    spriteBatch.Draw(target, new Rectangle(-offset, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);

                    if (loadingRoom is not null)
                    {
                        spriteBatch.Draw(loadingTarget, new Rectangle(Window.ClientBounds.Width - offset, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
                    }
                    break;
                case 2:
                    spriteBatch.Draw(target, new Rectangle(+offset, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);

                    if (loadingRoom is not null)
                    {
                        spriteBatch.Draw(loadingTarget, new Rectangle(0 - offsetBack, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
                    }
                    break;
                case 3:
                    //need to add room transition moving up
                case 4:
                    //need to add room transition moving down
                default:
                    spriteBatch.Draw(target, new Rectangle(-offset, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
                    break;
            }

            foreach (int key in TempBuffer.expiries)
            {
                TempBuffer.current[key].Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void SwitchRoom(int direction, Room room)
        {
            loadingDirection = direction;
            loadingRoom = room;
        }

        // Loading textures statically
        public static Texture2D Load(string path)
        {
            // Check if the file exists
            string fullPath = "Content/Sprites/" + path;
            if (!File.Exists(fullPath)) throw new FileNotFoundException($"Could not find texture at {fullPath}");
            // Try with resources
            using var fileStream = new FileStream(fullPath, FileMode.Open);
            return Texture2D.FromStream(device, fileStream);
        }

        // Loading textures with subimage
        public static Texture2D Load(string path, Rectangle subimage) => Subimage(Load(path), subimage);

        // Grabs a subimage from a texture
        public static Texture2D Subimage(Texture2D texture, Rectangle subimage)
        {
            Texture2D croppedTexture = new(device, subimage.Width, subimage.Height);
            Color[] data = new Color[subimage.Width * subimage.Height];
            texture.GetData(0, subimage, data, 0, data.Length);
            croppedTexture.SetData(data);
            return croppedTexture;
        }
    }
}
