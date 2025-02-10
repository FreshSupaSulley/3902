using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Game.ISprites;

namespace Game.Sprites
{
    public class motionAnimated : ISprite
    {
        public Texture2D texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private Vector2 position;
        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphics;
        private Vector2 startingPosition;
        private float timeElapsed;

        float speed = 3f;

        public motionAnimated(Texture2D texture, Vector2 position, int rows, int columns, GraphicsDeviceManager graphics)
        {
            this.texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            this.position = position;
            this.startingPosition = position;
            this.graphics = graphics;
        }

        public void Update(GameTime gameTime)
        {
            
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame--;
                currentFrame = 0;
            }

            /*
            position.X -= speed;

            if(position.X <= 0 || (position.X + (texture.Width / Columns)) >= graphics.PreferredBackBufferWidth)
            {
                speed = speed * (-1);
            }
            */
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = texture.Width / Columns;
            int height = texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            Thread.Sleep(60);
        }
    }
}
