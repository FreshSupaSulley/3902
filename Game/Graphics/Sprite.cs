using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// we could replace this with monogame's rectangle if this is all the functionality is going to be
namespace Game.Graphics
{
    // A Sprite is a single 2D image
    public class Sprite(Texture2D texture)
    {
        public Texture2D Texture { get; } = texture;
        private int width = texture.Width, height = texture.Height;

        public Sprite(Texture2D texture, int width, int height) : this(texture)
        {
            this.width = width;
            this.height = height;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, width, height), Color.White);//, sprite, Color.White);
        }
    }
}
