using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Sprites;
using Game.ISprites;

namespace Game.Sprites
{
    public class noMotionNoAnimated : ISprite
    {
        private Texture2D texture;
        private Vector2 position;
        private SpriteBatch spriteBatch;
        public noMotionNoAnimated(Texture2D texture, Vector2 position)
        { 
            this.texture = texture;
            this.position = position;
        }   

        public void Update(GameTime gametime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
