using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics
{
    // Animations hold a collection of Sprites from a spritesheet
    public class Animation
    {
        // Array of sprites composing this animation
        private readonly SpriteBatch batch;
        private readonly Rectangle[] sprites;
        private readonly Texture2D texture;
        // Speed of the animation
        private int duration;
        // Specifies which indices of the sprite array to cycle through during animation
        private int[] currentFrames;

        // Changes on tick
        private int index, frames;

        public Animation(Texture2D texture, int frames, int duration)
        {
            this.index = 0;
            this.texture = texture;
            this.frames = frames;
            this.duration = duration;
            sprites = new Rectangle[frames];
            int spriteWidth = texture.Width / frames;
            this.currentFrames = new int[frames];
            // Divide spriteSheet into sprites
            for (int i = 0; i < frames; i++)
            {
                sprites[i] = new Rectangle(i * spriteWidth, 0, spriteWidth, texture.Height);
                this.currentFrames[i] = i;
            }

        }
        
        public void reset(int[] an)
        {
            currentFrames = an;
            this.frames = an.Length;
            this.index = 0;
        }

        public Animation(Texture2D texture, Rectangle[] srcs)
        {
            this.index = 0;
            this.texture = texture;
            this.frames = srcs.Length;
            this.duration = 4; // default value for now, should be a param later
            this.sprites = srcs;
            int[] temp = new int[srcs.Length];
            for(int i = 0; i < srcs.Length; i++)
            {
                temp[i] = i;
            }
            this.currentFrames = temp;
        }

        public void Update()
        {
            index = Globals.cycle(this.index, this.currentFrames.Length);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // Draw sprite on the index
            // Draw the part of the animation we need
            Rectangle sprite = sprites[currentFrames[index]];
            // Draw at the center of the position
            spriteBatch.Draw(texture, position - new Vector2(sprite.Width / 2, sprite.Height / 2), sprite, Color.White);
        }

        public void Reset()
        {
            index = 0;
            frames = 0;
        }
    }
}