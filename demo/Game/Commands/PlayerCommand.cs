using Game.Entities;

namespace Game.Commands
{
    abstract class PlayerCommand : ICommand
    {
        public PlayerCommand(Player c)
        {
            player = c;
        }
        public Player player { get; set; }
        public abstract void Execute();
    }
}
