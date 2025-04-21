using System;

namespace Game.Commands;

class PauseCommand : ICommand {
    public PauseCommand() {

    }
    public void Execute() {
        if (Main.INSTANCE.State is State.Game) {
            Main.uiManager.SetGame((State.Game) Main.INSTANCE.State);
            Main.uiManager.ChangeUIState("pause"); 
        }
        else {
            Console.WriteLine("Failure to pause.");
        }
    }
}