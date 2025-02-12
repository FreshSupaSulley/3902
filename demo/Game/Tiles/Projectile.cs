using System.Numerics;
using Microsoft.Xna.Framework.Graphics;
using Game.Graphics;
using Game.Entities;

namespace Game.Tiles
{
    // We will find a use for this late
    public class Projectile : ITile
    {
        private static readonly Sprite dragon_projectaile = new Sprite(Game.Load("/Dragon/dragon_projectile.png"));
        float speed = 1f;
        private Vector2 position;
        private Vector2 startingPosition;

        public Projectile(Vector2 position)
        {
            this.position = position;
            startingPosition = position;
        }

        public void Update()
        {
            if(position.X < startingPosition.X-100)
            {

            }
            else
            {
                position.X -= speed;
            }
            
        }
        public void Draw(SpriteBatch batch)
        {
            dragon_projectaile.Draw(batch, new Vector2(position.X, 180));
            dragon_projectaile.Draw(batch, new Vector2(position.X, 200));
            dragon_projectaile.Draw(batch, new Vector2(position.X, 220));
        }
    }
}
