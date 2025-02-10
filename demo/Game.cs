using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game.Controllers;
using Game.Entities;
using Microsoft.Xna.Framework;
using Game.Graphics;
using demo.Game.Commands;

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
        private Dictionary<Keys, int> keyMappings = new Dictionary<Keys, int> {
            { Keys.NumPad1, 1 },
            { Keys.D1, 1 },
            { Keys.NumPad2, 2 },
            { Keys.D2, 2 },
            { Keys.NumPad3, 3 },
            { Keys.D3, 3 },
            { Keys.NumPad4, 4 },
            { Keys.D4, 4 }
        };

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
            // Controllers
            keyboard = new KeyboardController(this);
            mouse = new MouseController(this);
            // This calls load content
            base.Initialize();
        }

        protected override void LoadContent()
        {
            GraphicsDevice device = graphics.GraphicsDevice;
            AnimationRegistry.Load(device);
            // Font
            font = Content.Load<SpriteFont>("Font");
            Specs_h.monoko = Texture2D.FromFile(device, "C:\\Users\\tyfre\\source\\repos\\3902\\demo\\Content\\Sprites\\white_desert (edited).png");
            PlayerCharacter p = new PlayerCharacter();
            gameObjects.Add(p);
            //Make map for keyboard controller
            Dictionary<Keys, ICommand> m = new Dictionary<Keys, ICommand>();
            m.Add(Keys.Up, new PlayerMovementCommand(p, -1, 1));
            m.Add(Keys.Down, new PlayerMovementCommand(p, 1, 1));
            m.Add(Keys.Right, new PlayerMovementCommand(p, 1, 0));
            m.Add(Keys.Left, new PlayerMovementCommand(p, -1, 0));
            keyboard.map(m);

        }

        // Tick
        protected override void Update(GameTime gameTime)
        {
            // Probably unnecessary?
            base.Update(gameTime);

            // Always update inputs first
            keyboard.Update(Keyboard.GetState());
            mouse.Update(Mouse.GetState());

            // Quit functionality0
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape, Keys.D0, Keys.NumPad0) || mouse.RightDown()) Exit();

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

            // Draw quads
            for (int i = 0; i < 4; i++)
            {
                Vector2 quad = new(i % 2 * Window.ClientBounds.Width / 2, i / 2 * Window.ClientBounds.Height / 2);
                // Draw font
                Text("Quad " + (i + 1), quad + new Vector2(Window.ClientBounds.Width / 4, Window.ClientBounds.Height / 4));
                // Draw quad
                DrawOutlinedRect((int)quad.X, (int)quad.Y, Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2, Color.Black);
            }

            // Update each object
            foreach (IGameObject sample in gameObjects)
            {
                sample.Draw(spriteBatch);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void Text(string output, Vector2 position)
        {
            spriteBatch.DrawString(font, output, position, Color.Black, 0, font.MeasureString(output) / 2, 1, SpriteEffects.None, 0.5f);
        }

        // Draw all 4 corners of the outlined rect
        private void DrawOutlinedRect(int x, int y, int width, int height, Color color)
        {
            spriteBatch.Draw(pixel, new Rectangle(x, y, width, 1), color);
            spriteBatch.Draw(pixel, new Rectangle(x, y + height - 1, width, 1), color);
            spriteBatch.Draw(pixel, new Rectangle(x, y, 1, height), color);
            spriteBatch.Draw(pixel, new Rectangle(x + width - 1, y, 1, height), color);
        }
    }
}
