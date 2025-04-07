// using Game.Commands;

namespace Game.Commands;

public class ResumeCommand : ICommand {
    private State.Game game;
    private string newUI;
    public ResumeCommand(Game.State.Game game, string newUI) {
        this.game = game;
        this.newUI = newUI;
    }

    public void Execute() {
        Main.SwitchGameState(game);
        Main.uiManager.ChangeUIState(newUI);
        Main.uiManager.ClearGame();
    }

}