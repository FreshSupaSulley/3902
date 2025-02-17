using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game.Controllers;
using Game.Entities;
using Microsoft.Xna.Framework;
using Game.Commands;
using Game.Tiles;
using demo.Game;
using Game.Items;

namespace Game
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private static GraphicsDevice device;
        private GraphicsDeviceManager graphics;
        private KeyboardController keyboard;
        private MouseController mouse;

        private List<IGameObject> gameObjects = new List<IGameObject>();

        // Rectangle for quads
        private SpriteBatch spriteBatch;

        // Font
        private SpriteFont font;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Controllers
            keyboard = new KeyboardController(this);
            mouse = new MouseController(this);

            // This calls load content
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Game.device = graphics.GraphicsDevice;
            // Load Tiles
            Tile.LoadTextures();
            // Font
            font = Content.Load<SpriteFont>("Font");
            // Load player assets
            Monoko.monoko = Content.Load<Texture2D>("Sprites/white_desert (edited)"); 
            TempBuffer.pow = Content.Load<Texture2D>("Sprites/pow_(transparent)");
            Madotsuki.madoSpriteSheet = Content.Load<Texture2D>("Sprites/Mado");
            Lewa.texture = Content.Load<Texture2D>("Sprites/Lewa");
            Player p = new Player();
            Player p2 = new Player();
            gameObjects.Add(p);
            gameObjects.Add(p2);
            // Add dragon
            Dragon dragon = new Dragon();
            Gohma gohma = new Gohma();
            gameObjects.Add(dragon);
            // Add tile
            gameObjects.Add(new Tile(TileType.BRICK));
            // Add item
            gameObjects.Add(new Heart());
            
            // Add projectile
            gameObjects.Add(new Projectile(new System.Numerics.Vector2(200, 100)));

            ControllerLoader.LoadSprint2Commands(keyboard, p);
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
            TempBuffer.depreciate();

            // Quit functionality
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || keyboard.IsKeyDown(Keys.Escape) || keyboard.IsKeyDown(Keys.Q)
            || mouse.RightDown())
            {
                Exit();
            }

            // Update each object
            foreach (IGameObject sample in gameObjects)
            {
                sample.Update();
            }

            // Always end with post ticks
            keyboard.PostUpdate();
            mouse.PostUpdate();
        }

        // Render
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            // Update each object
            foreach (IGameObject sample in gameObjects)
            {
                sample.Draw(spriteBatch);
            }

            foreach (int key in TempBuffer.expiries)
            {
                TempBuffer.current[key].Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        // For loading textures statically
        public static Texture2D Load(string path)
        {
            // Try with resources
            using (var fileStream = new FileStream("Content/Sprites/" + path, FileMode.Open))
            {
                return Texture2D.FromStream(Game.device, fileStream);
            }
        }
    }
}
