namespace Game.Commands;

class MenuCommand : ICommand {
    public MenuCommand() {}
    public void Execute() {
        Main.uiManager.ChangeUIState("menu");
        Main.SwitchGameState(new State.Menu());
    }
}