using System.Numerics;
using Game.Entities;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    public class Item : IGameObject
    {
        private static readonly Sprite Heart = new Sprite(Game.Load("/Items/heart.png"));
        
        public void Update()
        {

        }
        public void Draw(SpriteBatch batch)
        {
            Heart.Draw(batch, new Vector2(100, 200));
        }
    }
}
