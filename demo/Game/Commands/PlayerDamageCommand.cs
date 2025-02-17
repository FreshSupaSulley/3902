using Game.Entities;

namespace Game.Commands
{
    internal class PlayerDamageCommand : PlayerCommand
    {
        public PlayerDamageCommand(Player p) : base(p)
        {

        }
        public override void Execute()
        {
            this.player.animate(this.player.animationSequences[Player.srcSprites.DAMAGED]);
            this.player.enforceDimensions();
        }
    }
}
