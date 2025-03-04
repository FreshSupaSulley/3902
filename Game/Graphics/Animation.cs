using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics
{
    // Animations hold a collection of Sprites from a spritesheet
    public class Animation
    {
        // Array of sprites composing this animation
        private readonly Rectangle[] sprites;
        private readonly Texture2D texture;
        // Speed of the animation
        private readonly int duration;

        // Changes on tick
        private int index, frames;

        public Animation(Texture2D texture, int frames, int duration)
        {
            this.texture = texture;
            this.frames = frames;
            this.duration = duration;
            sprites = new Rectangle[frames];
            int spriteWidth = texture.Width / frames;
            // Divide spriteSheet into sprites
            for (int i = 0; i < frames; i++)
            {
                sprites[i] = new Rectangle(i * spriteWidth, 0, spriteWidth, texture.Height);
            }
        }

        public void Update()
        {
            if (frames == duration)
            {
                frames = 0;
                index = (index + 1) % sprites.Length;
            }

            frames++;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // Draw the part of the animation we need
            Rectangle sprite = sprites[index];
            // When rendering sprites to the screen using coordinates that aren't integers, the spritebatch freaks out and the result looks like a blurred mess with some clipping issues
            // To reconcile, Animations will snap subpixels to pixels, but can create jarring movement in exchange for resolving the clipping issue.

            // dont draw centered anymore
            // spriteBatch.Draw(texture, new Vector2((int) Math.Round(position.X), (int) Math.Round(position.Y)) - new Vector2(sprite.Width / 2, sprite.Height / 2), sprite, Color.White);
            spriteBatch.Draw(texture, new Vector2((int) position.X, (int) position.Y), sprite, Color.White);
        }

        public void Reset()
        {
            index = 0;
            frames = 0;
        }
    }
}