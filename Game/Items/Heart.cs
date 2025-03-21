using System;
using Game.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    // Hearts dont do anything yet
    public class Heart(Vector2 position) : Item(position)
    {
        private static readonly Sprite SPRITE = new(Game.Load("/Items/zelda_items.png", new(0, 0, 7, 8)));

        public override void Update(Game game)
        {

        }

        public override void Draw(SpriteBatch batch)
        {
            SPRITE.Draw(batch, Position);
        }

        public override void Use(Game game)
        {
            Game.sfx["ding"].Play();
            Console.WriteLine("probably should heal player when we get health system");
        }
    }
}
