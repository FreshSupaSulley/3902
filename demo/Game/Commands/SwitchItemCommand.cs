using System;
using Game.Entities;
using Game.Items;

namespace Game.Commands
{
    internal class SwitchItemCommand(Player p, Func<Item> function) : PlayerCommand(p)
    {
        private Func<Item> function = function;

        public override void Execute()
        {
            player.Item = function();
        }
    }
}
