namespace Game.Commands;

public class StartGameCommand : ICommand {
    GraphicsDevice device;
    public StartGameCommand(GraphicsDevice device) {
        this.device = device;
    }
    public void Execute() {
        Main.SwitchGameState(new Game.State.Game(device))
    }
}