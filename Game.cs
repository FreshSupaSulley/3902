using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Game.Controllers;
using Game.Entities;
using Microsoft.Xna.Framework;
using Game.Tiles;
using Game.Rooms;

namespace Game
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        // Ratio
        public static Vector2 BASE_TO_WINDOW;

        // Used to load resources statically
        private static GraphicsDevice device;
        private GraphicsDeviceManager graphics;
        private RenderTarget2D target;

        // Used for rendering everything
        private SpriteBatch spriteBatch;
        private SpriteFont font;

        // Visible to inheritors
        public Player player;
        public Room room;
        public KeyboardController keyboard;
        public MouseController mouse;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1600 / 2,
                PreferredBackBufferHeight = 1100 / 2
            };
            // Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Size of Zelda map
            target = new RenderTarget2D(graphics.GraphicsDevice, (12 + 4) * 16, (7 + 4) * 16);
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
            // Used to load resources statically
            device = graphics.GraphicsDevice;
            Tile.LoadTextures();
            font = Content.Load<SpriteFont>("Font");
            // Player object that stays around throughout session
            player = new();
            // Add room
            room = new DragonRoom(player);
            // Pow
            TempBuffer.pow = Load("pow.png");
            // Add doors
            //gameObjects.Add(door = new Door(DoorType.OPEN));
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

            // Tick room
            room.Update(this);

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

            // Switch back to main backbuffer and draw buffer
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            spriteBatch.Draw(target, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);

            foreach (int key in TempBuffer.expiries)
            {
                TempBuffer.current[key].Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
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

        // Loading textures with subimages
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
