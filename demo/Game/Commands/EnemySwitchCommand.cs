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
        private List<IGameObject> gameObjects;

        public EnemySwitchCommand(int state, MobileEntity[] entities, List<IGameObject> gameObjects)
        {
            this.state = state;
            this.entities = entities;
            this.gameObjects = gameObjects;
        }

        public override void Execute()
        {
            foreach (Entity el in entities) {
                gameObjects.Remove(el);
            }
            gameObjects.Add(this.entities[state]);
        }
    }
}
