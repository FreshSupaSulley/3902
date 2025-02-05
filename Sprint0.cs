using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Controllers;

namespace Sprint0;

public class Sprint0 : Game
{
    private GraphicsDeviceManager graphics;
    private KeyboardController keyboard;
    private MouseController mouse;

    // Animations
    private Vector2 center;
    private Animation active, idle, run;
    private int animationIndex;

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

    public Sprint0()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        center = graphics.GraphicsDevice.Viewport.Bounds.Center.ToVector2();
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
        // Define animation
        idle = new(center, "idle.png", device, 1, 1);
        run = new(center, "run.png", device, 3, 3);
        active = idle;
    }

    // Tick
    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        keyboard.Tick();
        mouse.Tick();

        // Quit functionality0
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape, Keys.D0, Keys.NumPad0) || mouse.RightDown()) Exit();

        int newIndex = keyMappings.FirstOrDefault(pair => keyboard.IsKeyPressed(pair.Key)).Value;
        animationIndex = newIndex != 0 ? newIndex : animationIndex;

        // Mouse takes priority over keyboard input
        if (mouse.LeftDown())
        {
            bool left = mouse.RelativeX() <= 0.5 && mouse.RelativeX() > 0;
            bool top = mouse.RelativeY() <= 0.5 && mouse.RelativeY() > 0;

            if (left && top)
            {
                animationIndex = 1;
            }
            else if (!left && top)
            {
                animationIndex = 2;
            }
            else if (left && !top)
            {
                animationIndex = 3;
            }
            else if (!left && !top)
            {
                animationIndex = 4;
            }
        }

        switch (animationIndex)
        {
            // Have a key (1) that has the program display a sprite with only one frame of animation and a fixed position. This should be the initial state of the program.
            case 1:
                {
                    SetAnimation(idle);
                    break;
                }
            // Have a key (2) that has the program display an animated sprite, but with a fixed position.
            case 2:
                {
                    SetAnimation(run);
                    break;
                }
            // Have a key (3) that has the program display a sprite with only one frame of animation, but moves the sprite up and down on screen.
            case 3:
                {
                    SetAnimation(idle);
                    active.Position = center + new Vector2(0, (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 1000) * 100);
                    break;
                }
            // Have a key (4) that has the program display an animated sprite, moving to the left and right on screen.
            case 4:
                {
                    SetAnimation(run);
                    active.Position = center + new Vector2((float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 1000) * 100, 0);
                    break;
                }
        }

        // Tick active animation
        active.Tick();

        // Update saved state
        keyboard.PostTick();
        mouse.PostTick();
    }

    private void SetAnimation(Animation animation)
    {
        active.Position = center;
        // Ignore if already set
        if (animation == active) return;
        active.Reset();
        active = animation;
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
        // Draw animation
        active.Render();
        // Credits
        Text("Program Made By: Erich Boschert\nSprites from: mariouniverse.com/sprites-nes-smb/", new(200, 50));
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
