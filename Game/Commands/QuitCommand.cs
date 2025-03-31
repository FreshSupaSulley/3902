using Game.State;

namespace Game.Commands;

class QuitCommand : ICommand
{
	private State.Game game;
	public QuitCommand(State.Game game)
	{
		this.game = game;
	}

	public void Execute()
	{
		// what?
		// game.Exit();
	}
}