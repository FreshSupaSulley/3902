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
    public class Heart : Item
    {
        private static readonly Sprite SPRITE = new(Main.Load("/Items/zelda_items.png", new(0, 0, 7, 8)));
        private Player player;
        public Heart(Vector2 position, Player player): base(position) {
            this.player = player;
        }

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
