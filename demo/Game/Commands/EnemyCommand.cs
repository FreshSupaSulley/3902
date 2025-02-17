using Game.Entities;

namespace Game.Commands
{
    abstract class EnemyCommand : ICommand
    {
        public EnemyCommand()
        {
            
        }
        public Dragon dragon { get; set; }
        public abstract void Execute();
    }
}
