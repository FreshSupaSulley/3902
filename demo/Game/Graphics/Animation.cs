using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics
{
    // Animations hold a collection of Sprites from a spritesheet
    public class Animation
    {
        // Array of sprites composing this animation
        private readonly SpriteBatch batch;
        private readonly Sprite[] sprites;
        private readonly Texture2D texture;
        // Speed of the animation
        private int[][] animations;
        public int[] currentAnimation { get; set; }
        private int currentFrame = 0;
        // Changes on tick
        private int index, frames;

        //default constructor - assumes that 
        public Animation(Texture2D texture, Microsoft.Xna.Framework.Rectangle[] srcs) {
            this.texture = texture;
            sprites = new Sprite[srcs.Length];
            //Initial implementation assumed equal dimensions of all sprites, changed so that positions of sprites are hard-coded
            for (int i = 0; i < srcs.Length; i++)
            {
                sprites[i] = new Sprite(srcs[i].X, srcs[i].Y, srcs[i].Width, srcs[i].Height);
            }
            currentAnimation = new int[] { 0 };
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // Draw sprite on the index
            // Draw the part of the animation we need
            Sprite sprite = sprites[currentAnimation[currentFrame]];
            currentFrame = Specs_h.cycle(currentFrame, currentAnimation.Length);
            // Draw at the center of the position
            spriteBatch.Draw(texture, position - new Vector2(sprite.Width / 2, sprite.Height / 2), new Rectangle(sprite.X, sprite.Y, sprite.Width, sprite.Height), Color.White);
        }

        public void Reset()
        {
            currentFrame = 0;
        }
    }
}