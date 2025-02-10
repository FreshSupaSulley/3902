using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.ISprites;

namespace Game.Sprites
{
    public class motionNoAnimated : ISprite
    {
        private Texture2D texture;
        private Vector2 position;
        private SpriteBatch spriteBatch;
        private Vector2 startingPosition;
        private float timeElapsed;

        public motionNoAnimated(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            this.startingPosition = position;
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            position = new Vector2(position.X, startingPosition.Y + 20 * (float)Math.Sin(timeElapsed * 3f));
        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
