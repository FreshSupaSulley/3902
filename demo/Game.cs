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
using System;

namespace Game
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private static GraphicsDevice device;
        private GraphicsDeviceManager graphics;
        private KeyboardController keyboard;
        private MouseController mouse;

        private List<IGameObject> gameObjects = new List<IGameObject>();

        // Demo objects
        private Tile tile;
        private Item item;
        public int itemIndex;

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
            gameObjects.Add(p);
            // Add dragon
            Dragon dragon = new Dragon();
            Gohma gohma = new Gohma();
            gameObjects.Add(dragon);
            // Add tile
            gameObjects.Add(tile = new Tile(TileType.BRICK));
            // Setup item
            item = new Heart{ Position = new(600, 200) };

            // Add projectile
            gameObjects.Add(new Projectile(new System.Numerics.Vector2(200, 100)));

            MobileEntity[] entities = { dragon, gohma };

            ControllerLoader.LoadSprint2Commands(tile, this, keyboard, p, entities, gameObjects);
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

            // Reset functionality
            if (keyboard.IsKeyPressed(Keys.R))
            {
                // Reset item
                item = new Heart{ Position = new(600, 200) };
                itemIndex = 0;
                // Remove items placed by player
                gameObjects.RemoveAll(obj => obj is Item);
                gameObjects.Add(item);
            }

            // Update each object
            foreach (IGameObject sample in gameObjects)
            {
                sample.Update();
            }

            // Tick item, special behavior
            item.Update();

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

            // Render item, special behavior
            item.Draw(spriteBatch);

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
                return Texture2D.FromStream(device, fileStream);
            }
        }

        public static Texture2D Load(string path, Rectangle subimage)
        {
            Texture2D source = Load(path);
            Texture2D croppedTexture = new(device, subimage.Width, subimage.Height);
            Color[] data = new Color[subimage.Width * subimage.Height];
            source.GetData(0, subimage, data, 0, data.Length);
            croppedTexture.SetData(data);
            return croppedTexture;
        }

        public void AddGameObject(Item item)
        {
            gameObjects.Add(item);
        }

        public void SetDemoItem(Item item)
        {
            this.item = item;
            this.item.Position = new Vector2(600, 200);
        }
    }
}
