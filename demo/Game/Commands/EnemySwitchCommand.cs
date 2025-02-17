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
        private int state;

        private Entity[] entities;

        public EnemySwitchCommand(int state, MobileEntity[] entities)
        {
            this.state = state;
            this.entities = entities;
        }

        public override void Execute()
        {
            
            entities[state].Update();
        }
    }
}
