using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Commands;
using Game.Graphics;

namespace demo.Game.Commands
{
    internal class PlayerAttackCommand : PlayerCommand
    {
        public PlayerAttackCommand(Player p): base(p) { }
        public override void Execute() {
            this.player.animate(new int[] { 3 });
            this.player.attackFlag = true;
            TempBuffer.add(new Entities.TempEntity(TempBuffer.pow, player.Position), 5000);
        }
    }
}
