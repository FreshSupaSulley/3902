using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

public class Animation
{
    // Array of sprites composing this animation
    private SpriteBatch batch;
    private ISprite[] sprites;
    private Texture2D spriteSheet;
    // Speed of the animation
    private readonly int duration;
    // Changes on tick
    private int index, frames;

    // Public position (will probably change later)
    public Vector2 Position { get; set; }

    public Animation(Vector2 position, string path, GraphicsDevice device, int frames, int duration)
    {
        this.Position = position;
        this.frames = frames;
        this.duration = duration;
        // Setup spritebatch
        batch = new SpriteBatch(device);
        // Try with resources
        using (var fileStream = new FileStream("Content/Sprites/" + path, FileMode.Open))
        {
            spriteSheet = Texture2D.FromStream(device, fileStream);
        }
        sprites = new ISprite[frames];
        int spriteWidth = spriteSheet.Width / frames;
        // Divide spriteSheet into sprites
        for(int i = 0; i < frames; i++)
        {
            sprites[i] = new Sprite(i * spriteWidth, 0, spriteWidth, spriteSheet.Height);
        }
    }

    public void Tick()
    {
        if(frames == duration)
        {
            frames = 0;
            index = (index + 1) % sprites.Length;
        }
        frames++;
    }

    public void Render()
    {
        // Draw sprite on the index
        batch.Begin();
        // Draw the part of the animation we need
        ISprite sprite = sprites[index];
        // Draw at the center of the position
        batch.Draw(spriteSheet, Position - new Vector2(sprite.Width / 2, sprite.Height / 2), new Rectangle(sprite.X, sprite.Y, sprite.Width, sprite.Height), Color.White);
        // End
        batch.End();
    }

    public void Reset()
    {
        index = 0;
        frames = 0;
    }
}