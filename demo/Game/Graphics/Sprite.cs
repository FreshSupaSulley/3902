using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// we could replace this with monogame's rectangle if this is all the functionality is going to be
namespace Game.Graphics
{
    // A Sprite is a single 2D image
    public class Sprite
    {
        private readonly Texture2D texture;

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }
        
        public Sprite(Texture2D texture, Rectangle subimage) : this(texture) {}
        
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, position, Color.White);//, sprite, Color.White);
        }
    }
}
