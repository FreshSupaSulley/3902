using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game.Controllers;
using Game.Entities;
using Microsoft.Xna.Framework;
using Game.Graphics;
using Game.Commands;
using Game.Tiles;

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
            Game.device = graphics.GraphicsDevice;
            // Font
            font = Content.Load<SpriteFont>("Font");
            Globals.monoko = Content.Load<Texture2D>("Sprites/white_desert (edited)");
            Player p = new Player();
            gameObjects.Add(p);
            // Add dragon
            gameObjects.Add(new Dragon());
            // Add tile
            gameObjects.Add(new Brick());
            //Make map for keyboard controller
            Dictionary<Keys, ICommand> m = new Dictionary<Keys, ICommand>(); 
            ICommand up = new PlayerMovementCommand(p, -1, 1);
            ICommand down = new PlayerMovementCommand(p, 1, 1);
            ICommand right = new PlayerMovementCommand(p, 1, 0);
            ICommand left = new PlayerMovementCommand(p, -1, 0);
            ICommand revert = new PlayerMovementCommand(p, 0, 0);
            m.Add(Keys.Up, up);
            m.Add(Keys.Down, down);
            m.Add(Keys.Right, right);
            m.Add(Keys.Left, left);
            m.Add(Keys.W, up);
            m.Add(Keys.S, down);
            m.Add(Keys.A, left);
            m.Add(Keys.D, right);
            m.Add(Keys.F13, revert);
            keyboard.AddCommand(m);
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
