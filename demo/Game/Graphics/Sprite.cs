using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// we could replace this with monogame's rectangle if this is all the functionality is going to be
namespace Game.Graphics
{
    // A Sprite is a single 2D image
    public class Sprite(Texture2D texture)
    {
        public Texture2D Texture { get; } = texture;

        public Sprite(Texture2D texture, Rectangle subimage) : this(texture) { }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position, Color.White);//, sprite, Color.White);
        }
    }
}
