using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game.Controllers;
using Microsoft.Xna.Framework;
using Game.Sprites;
using Game.ISprites;

namespace Game
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private KeyboardController keyboard;
        private MouseController mouse;

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

        private ISprite playerSprite;
        private ISprite obstacleSprite;
        private ISprite enemySprite;
        private ISprite itemSprite;

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
            // Font
            font = Content.Load<SpriteFont>("Font");
            // Entities should not be initialized here but have for now due to asset loading ordering someone help
            Texture2D wallTexture = Content.Load<Texture2D>("wall");
            Texture2D enemyTexture = Content.Load<Texture2D>("enemy");
            Texture2D playerTexture = Content.Load<Texture2D>("link");
            Texture2D itemTexture = Content.Load<Texture2D>("heart");

            ISprite wall = new noMotionNoAnimated(wallTexture, new Vector2(100,100));
            ISprite heart = new noMotionNoAnimated(itemTexture, new Vector2(100, 200));
            ISprite enemy = new noMotionAnimated(enemyTexture, new Vector2(300, 300), 1, 4);
            ISprite player = new motionAnimated(playerTexture, new Vector2(200, 200), 1, 2, graphics);

            obstacleSprite = wall;
            enemySprite = enemy;
            playerSprite = player;
            itemSprite = heart;
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || KeyboardController.IsKeyDown(Keys.Escape, Keys.D0, Keys.NumPad0) || mouse.RightDown()) Exit();

            enemySprite.Update(gameTime);
            playerSprite.Update(gameTime); 
            // Always end with post ticks
            keyboard.PostUpdate();
            mouse.PostUpdate();
        }

        // Render
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            obstacleSprite.Draw(spriteBatch);
            enemySprite.Draw(spriteBatch);
            playerSprite.Draw(spriteBatch);
            itemSprite.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
