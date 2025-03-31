using Microsoft.Xna.Framework.Graphics;
using Game.Controllers;
using Game.Entities;
using Microsoft.Xna.Framework;
using Game.Tiles;
using Game.Rooms;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Game.State
{
    public class Game : IGameState
    {
        // Used to load resources statically
        private GraphicsDevice device;
        private RenderTarget2D target, loadingTarget;

        // Visible to inheritors
        public Player player;
        public Room room;
        public KeyboardController keyboard;
        public MouseController mouse;

        // Pertains to loading rooms
        private readonly int TRANSITION_TIME = 60;
        private bool renderCaptured;
        private int loadingTime, loadingDirection;
        private Room loadingRoom;

        public static Game instance;

        //sound
        //sound effects to be played once (i.e. without looping)
        public static Dictionary<String, SoundEffect> sfx = new Dictionary<string, SoundEffect>();
        //currently playing music
        public static SoundEffectInstance bgm;

        public Game()
        {
            instance = this;
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

            BASE_TO_WINDOW = new Vector2(graphics.PreferredBackBufferWidth / target.Bounds.Width, graphics.PreferredBackBufferHeight / target.Bounds.Height);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Controllers
            keyboard = new KeyboardController(this);
            mouse = new MouseController(this);

            // This calls load content
            base.Initialize();
        }

        protected override void LoadContent()
        {
            loadSoundEffect("ding.wav");
            loadSoundEffect("punch.wav");
            changeMusic("Song_1.wav");
            // Used to load resources statically
            device = graphics.GraphicsDevice;
            Tile.LoadTextures();
            Door.LoadTextures();
            // Load start room. This also defines the player
            room = Room.LoadRoom("start");
            this.player = (Player)room.gameObjects.Find(entity => entity is Player);
            // Pow
            TempBuffer.pow = Main.Load("pow.png");
        }

        public void Update(GameTime gameTime)
        {
            // If we want to switch to pause
            if (Main.INSTANCE.keyboard.IsKeyPressed(Keys.Escape))
            {
                Main.SwitchGameState(new Pause(this));
                return;
            }
            
            // Tick room if not mid-transition
            if (loadingRoom is null)
            {
                room.Update(this);
            }
            // If we are switching between rooms
            else
            {
                if (loadingTime++ >= TRANSITION_TIME)
                {
                    room = loadingRoom;
                    loadingRoom = null;
                    loadingTime = 0;
                    renderCaptured = false;
                }
            }
        }

        // Render
        public void Draw(SpriteBatch spriteBatch)
        {
            // If loading
            if (loadingRoom is null)
            {
                // Draw room at scaled resolution
                device.SetRenderTarget(target);
                spriteBatch.Begin();
                room.Draw(spriteBatch);
                spriteBatch.End();
            }
            else if (renderCaptured is false)
            {
                // Put player in correct location
                switch (loadingDirection)
                {
                    // Top
                    case 0:
                        player.ActiveAnimation = Player.DOWN;
                        player.Position = new(144 - (player.collisionBox.Width + player.collisionBox.X * 2 + 32) / 2, 32 - player.collisionBox.Y);
                        break;
                    // Bottom
                    case 2:
                        player.ActiveAnimation = Player.UP;
                        player.Position = new(144 - (player.collisionBox.Width + player.collisionBox.X * 2 + 32) / 2, 144 - player.collisionBox.Y - player.collisionBox.Height);
                        break;
                    // Left
                    case 3:
                        player.ActiveAnimation = Player.RIGHT;
                        player.Position = new(32 - player.collisionBox.X, 104 - (player.collisionBox.Height + player.collisionBox.Y * 2 + 32) / 2);
                        break;
                    // Right
                    case 1:
                        player.ActiveAnimation = Player.LEFT;
                        player.Position = new(224 - player.collisionBox.X - player.collisionBox.Width, 104 - (player.collisionBox.Height + player.collisionBox.Y * 2 + 32) / 2);
                        break;
                }
                renderCaptured = true;
                device.SetRenderTarget(loadingTarget);
                spriteBatch.Begin();
                loadingRoom.Draw(spriteBatch);
                spriteBatch.End();
            }

            // Number of pixels
            double sin = Math.Pow(Math.Sin(loadingTime * Math.PI / 2 / TRANSITION_TIME), 2);
            // Switch back to main backbuffer
            device.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            var bounds = Main.INSTANCE.Window.ClientBounds;
            switch (loadingDirection)
            {
                // Bottom going to top
                case 0:
                    {
                        int offset = (int)(sin * bounds.Height);
                        spriteBatch.Draw(target, new Rectangle(0, -offset, bounds.Width, bounds.Height), Color.White);
                        if (loadingRoom is not null)
                        {
                            spriteBatch.Draw(loadingTarget, new Rectangle(0, bounds.Height - offset, bounds.Width, bounds.Height), Color.White);
                        }
                        break;
                    }
                case 1:
                    {
                        int offset = (int)(sin * bounds.Width);
                        spriteBatch.Draw(target, new Rectangle(offset, 0, bounds.Width, bounds.Height), Color.White);
                        if (loadingRoom is not null)
                        {
                            spriteBatch.Draw(loadingTarget, new Rectangle(offset - bounds.Width, 0, bounds.Width, bounds.Height), Color.White);
                        }
                        break;
                    }
                // Top going to bottom
                case 2:
                    {
                        int offset = (int)(sin * bounds.Height);
                        spriteBatch.Draw(target, new Rectangle(0, offset, bounds.Width, bounds.Height), Color.White);
                        if (loadingRoom is not null)
                        {
                            spriteBatch.Draw(loadingTarget, new Rectangle(0, offset - bounds.Height, bounds.Width, bounds.Height), Color.White);
                        }
                        break;
                    }
                case 3:
                    {
                        int offset = (int)(sin * bounds.Width);
                        spriteBatch.Draw(target, new Rectangle(-offset, 0, bounds.Width, bounds.Height), Color.White);
                        if (loadingRoom is not null)
                        {
                            spriteBatch.Draw(loadingTarget, new Rectangle(bounds.Width - offset, 0, bounds.Width, bounds.Height), Color.White);
                        }
                        break;
                    }
            }

            foreach (int key in TempBuffer.expiries)
            {
                TempBuffer.current[key].Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        public void SwitchRoom(int direction, Room room)
        {
            loadingDirection = direction;
            loadingRoom = room;
            // Force add player into room. Delete player object if it already had one
            loadingRoom.gameObjects.RemoveAll(item => item is Player);
            loadingRoom.gameObjects.Add(player);
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

        //loads sound into sfx
        public static void loadSoundEffect(String filename){
            if(filename.IndexOf(".") > 0){
              String name = filename.Substring(0, filename.IndexOf("."));
             string path = "Content/Sound/" + filename;
             if(!File.Exists(path)) throw new FileNotFoundException($"Could not find sound at {path}");
             sfx.Add(name, SoundEffect.FromStream(new FileStream(path, FileMode.Open)));
            }else{
                Console.WriteLine("Error: invalid name formatting for sound file");
            }
        }
        //changes background music
        public static void changeMusic(String filename){
            if(filename.IndexOf(".") > 0){
              String name = filename.Substring(0, filename.IndexOf("."));
             string path = "Content/Sound/" + filename;
             if(!File.Exists(path)) throw new FileNotFoundException($"Could not find sound at {path}");
             bgm = SoundEffect.FromStream(new FileStream(path, FileMode.Open)).CreateInstance();
             bgm.IsLooped = true;
             bgm.Play();
            }else{
                Console.WriteLine("Error: invalid name formatting for sound file");
            }
        }

        public static void reset(){
            Game.instance.SwitchRoom(1, Room.LoadRoom("start"));
        }
    }
}
