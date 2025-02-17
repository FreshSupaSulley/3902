using Game.Commands;
using Microsoft.Xna.Framework;

namespace demo.Game.Commands
{
    internal class PlayerAttackCommand : PlayerCommand
    {
        public PlayerAttackCommand(Player p): base(p) { }
        public override void Execute() {
            if (player.attackFlag)
            {
                this.player.animate(this.player.attack[this.player.direction]);
            }
            else
            {
                this.player.animate(player.animationSequences[Player.srcSprites.ATTACK]);
            }
            TempBuffer.add(new Entities.TempEntity(TempBuffer.pow, player.Position), 5000);
        }
    }
}
