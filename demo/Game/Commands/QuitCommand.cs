// using Microsoft.Xna.Framework; // Possibly necessary in the future

namespace Game.Commands;

class QuitCommand : ICommand {
	private Game game;
	public QuitCommand(Game game) {
		this.game = game;
	}
	public void Execute() {
		game.Exit();
	}
}