using System.Numerics;
using Game.Entities;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    public class Heart : IItem
    {
        private static readonly Sprite SPRITE = new Sprite(Game.Load("/Items/heart.png"));

        public void Use() => throw new System.NotImplementedException();

        public void Update()
        {

        }

        public void Draw(SpriteBatch batch)
        {
            SPRITE.Draw(batch, new Vector2(100, 200));
        }
    }
}
