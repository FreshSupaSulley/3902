using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Numerics;
using Game.Entities;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Security.Principal;

namespace Game.Entities
{
    public class Dragon : MobileEntity
    {
        private static readonly Animation IDLE = new Animation(Game.Load("/Dragon/dragon.png"), 4, 10);
        private static readonly Animation Hurt = new Animation(Game.Load("/Dragon/dragon_hurt.png"), 1, 1);

        public Dictionary<int, Animation> animationSequences { get; set; }
        private Animation prev;
        private int ticks;

        public Dragon() : base(new Vector2(200, 200), IDLE) {
            animationSequences = new Dictionary<int, Animation>();
            animationSequences.Add(0, IDLE);
            animationSequences.Add(1, Hurt);
        }

        public void animate(int i)
        {
            prev = this.activeAnimation;
            this.activeAnimation = animationSequences[i];

        }

        public override void Update()
        {
            base.Update();

            ticks++;
            Position += new Vector2(0, ticks / 10 % 2 == 0 ? 1 : -1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
