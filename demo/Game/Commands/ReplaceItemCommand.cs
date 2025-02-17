using Game.Items;

namespace Game.Commands
{
    internal class ReplaceItemCommand(Game game, bool forwards) : ICommand
    {
        private readonly Game game = game;
        private static readonly Item[] items = {new Heart(), new Boomerang(), new Bomb()};

        public void Execute()
        {
            game.itemIndex = (game.itemIndex + (forwards ? 1 : -1) + items.Length) % items.Length;
            game.SetDemoItem(items[game.itemIndex]);
        }
    }
}
