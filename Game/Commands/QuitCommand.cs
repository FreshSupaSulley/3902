using Game.State;

namespace Game.Commands;

class QuitCommand : ICommand
{
	private Microsoft.Xna.Framework.Game game;
	public QuitCommand(Microsoft.Xna.Framework.Game game)
	{
		this.game = game;
	}

	public void Execute()
	{
		game.Exit();
	}
}