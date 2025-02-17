using Game.Entities;

namespace Game.Commands
{
    internal class PlayerAttackCommand(Game game, Player p) : PlayerCommand(p)
    {
        private readonly Game game = game;

        public override void Execute()
        {
            if (player.attackFlag)
            {
                this.player.animate(this.player.attack[this.player.direction]);
            }
            else
            {
                this.player.animate(player.animationSequences[Player.srcSprites.ATTACK]);
            }
            player.UseItem(game);
            TempBuffer.add(new TempEntity(TempBuffer.pow, player.Position), 5000);
        }
    }
}
