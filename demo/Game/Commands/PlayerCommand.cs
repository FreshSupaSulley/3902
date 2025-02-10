using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Commands
{
    abstract class PlayerCommand : ICommand
    {
        public PlayerCommand(PlayerCharacter c) {
            player = c;
        }
        public PlayerCharacter player { get; set; }
        public abstract void Execute();
    }
}
