using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game.Controllers;
using Game.Entities;
using Microsoft.Xna.Framework;
using Game.Graphics;
using Game.Commands;

namespace Game
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private KeyboardController keyboard;
        private MouseController mouse;

        private List<IGameObject> gameObjects = new List<IGameObject>();

        // Rectangle for quads
        private SpriteBatch spriteBatch;
        private Texture2D pixel;

        // Font
        private SpriteFont font;

        // Map keys to an animation (too specific for keyboardcontroller)
        private Dictionary<Keys, ICommand> keyMappings;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Drawing rects
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.Black });


            // Setting Up Key Mappings
            keyMappings = new Dictionary<Keys, ICommand> {
                { Keys.NumPad0, new QuitCommand(this) },
                { Keys.D0, new QuitCommand(this) },
            };

            // Controllers
            keyboard = new KeyboardController(this);
            mouse = new MouseController(this);

            keyboard.AddCommand(keyMappings);

            // This calls load content
            base.Initialize();
        }

        protected override void LoadContent()
        {
            GraphicsDevice device = graphics.GraphicsDevice;
            AnimationRegistry.Load(device);
            // Font
            font = Content.Load<SpriteFont>("Font");
            // Entities should not be initialized here but have for now due to asset loading ordering someone help
            gameObjects.Add(new DemoPlayer(new System.Numerics.Vector2(graphics.GraphicsDevice.Viewport.Bounds.Center.X, graphics.GraphicsDevice.Viewport.Bounds.Center.Y)));
        }

        // Tick
        protected override void Update(GameTime gameTime)
        {
            // Probably unnecessary?
            base.Update(gameTime);

            // Always update inputs first
            keyboard.Update();
            mouse.Update();

            // Quit functionality0
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
            || keyboard.IsKeyDown(Keys.Escape) 
            || mouse.RightDown()) {
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
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
