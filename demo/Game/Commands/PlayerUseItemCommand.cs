using Game.Entities;

namespace Game.Commands
{
    internal class PlayerUseItemCommand(Player p, Game game) : PlayerCommand(p)
    {
        private readonly Game game = game;

        public override void Execute()
        {
            this.player.UseItem(game);
        }
    }
}
