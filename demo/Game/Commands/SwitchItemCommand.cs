using Game.Commands;
using Game.Items;

namespace demo.Game.Commands
{
    internal class SwitchItemCommand : PlayerCommand
    {
        private IItem item;
        public SwitchItemCommand(Player p, IItem item): base(p) {
            this.item = item;
        }
        public override void Execute() {
            player.Item = item;
        }
    }
}
