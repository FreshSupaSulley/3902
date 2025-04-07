using System;
using Game.Entities;
using Game.Graphics;
using Game.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    // Hearts dont do anything yet
    public class Key : LivingEntity
    {
        private static readonly Animation SPRITE = new(Main.Load("/Items/key.png", new(0, 0, 7, 16)),1,10);

        public Key( ) : base(10,new Rectangle(0,0,10,16),SPRITE)
        {
        }

        public override void Update(State.Game game)
        {
            SPRITE.Update();
        }
        public override Vector2 Move(State.Game game)
        {
            return Vector2.Zero;
        }

        public override void Draw(SpriteBatch batch)
        {
            SPRITE.Draw(batch, Position);
        }

        public void Use(State.Game game)
        {
            game.sfx["ding"].Play();
            Console.WriteLine("probably should heal player when we get health system");
        }

       
    }
}
