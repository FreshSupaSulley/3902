using Game.State;

namespace Game.Commands;

class QuitCommand : ICommand
{
	private World game;
	public QuitCommand(World game)
	{
		this.game = game;
	}

	public void Execute()
	{
		// what?
		// game.Exit();
	}
}