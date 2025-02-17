using Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Commands
{
    abstract class EnemyCommand : ICommand
    {
        public EnemyCommand()
        {
            
        }
        public Dragon dragon { get; set; }
        public abstract void Execute();
    }
}
