using Microsoft.Xna.Framework.Graphics;

namespace Game.Commands;

public class StartGameCommand : ICommand {
    private GraphicsDevice device;
    private string newUI;
    public StartGameCommand(GraphicsDevice device) : this(device, null) {}
    public StartGameCommand(GraphicsDevice device, string newUI) {
        this.device = device;
        this.newUI = newUI;
    }
    public void Execute() {
        Main.SwitchGameState(new Game.State.Game(device));
        Main.uiManager.ChangeUIState(this.newUI);
    }
}