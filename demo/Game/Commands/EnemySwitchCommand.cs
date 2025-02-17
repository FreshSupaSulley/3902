using Game.Entities;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Commands
{
    internal class EnemySwitchCommand : EnemyCommand
    {
        int state;

        private static Animation IDLE = new Animation(Game.Load("/Dragon/dragon.png"), 4, 10);
        private static Animation Hurt = new Animation(Game.Load("/Dragon/dragon_hurt.png"), 1, 10);

        public EnemySwitchCommand(int state)
        {
            this.state = state;
        }

        public override void Execute()
        {
            dragon.animationSequences = new Dictionary<int, Animation>();
            dragon.animationSequences.Add(0, IDLE);
            dragon.animationSequences.Add(1, Hurt);

            dragon.animate(state);
            dragon.Update();
        }
    }
}
