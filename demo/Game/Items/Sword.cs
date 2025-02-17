using System;
using System.Numerics;
using Game.Entities;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    public class Sword : IItem
    {
        private static readonly Sprite SPRITE = new Sprite(Game.Load("/Items/boomerang.png"));

        public override void Use()
        {
            Console.WriteLine("HI");
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch batch)
        {
            SPRITE.Draw(batch, new Vector2(100, 200));
        }
    }
}
