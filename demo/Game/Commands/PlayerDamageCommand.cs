using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Commands;
namespace demo.Game.Commands
{
    internal class PlayerDamageCommand : PlayerCommand
    {
        public PlayerDamageCommand(Player p) : base(p)
        {

        }
        public override void Execute()
        {
            this.player.animate(new int[] { Globals.mkAll.Length - 1 });
            this.player.enforceDimensions();
        }
    }
}
