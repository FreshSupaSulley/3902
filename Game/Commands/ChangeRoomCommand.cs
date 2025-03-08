using Game.Rooms;

namespace Game.Commands;

class ChangeRoomCommand : ICommand {
    private int doorAlignment;
    private Room destinationRoom;
    private Game game;
    public ChangeRoomCommand(int doorAlignment, Room destinationRoom, Game game) {
        this.doorAlignment = doorAlignment;
        this.destinationRoom = destinationRoom;
        this.game = game;
    }
    public void Execute() {
        game.SwitchRoom(doorAlignment, destinationRoom);
    }
}