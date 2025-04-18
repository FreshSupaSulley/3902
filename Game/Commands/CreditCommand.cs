namespace Game.Commands;

class CreditCommand : ICommand {
    public CreditCommand() {}
    public void Execute() {
        Main.uiManager.ChangeUIState("credits");
        Main.SwitchGameState(new State.Credits());
    }
}