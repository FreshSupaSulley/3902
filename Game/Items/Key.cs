using System;
using Game.Graphics;
using Game.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    // Hearts dont do anything yet
    public class Key(Vector2 position) : Item(position)
    {
        private static readonly Sprite SPRITE = new(Main.Load("/Items/key.png", new(0, 0, 7, 8)));

        public override void Update(State.Game game)
        {

        }

        public override void Draw(SpriteBatch batch)
        {
            SPRITE.Draw(batch, Position);
        }

        public override void Use(State.Game game)
        {
            game.sfx["ding"].Play();
            Console.WriteLine("probably should heal player when we get health system");
        }
    }
}
