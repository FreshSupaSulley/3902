using Game.Rooms;
using Game.State;

namespace Game.Commands;

class ChangeRoomCommand : ICommand
{
    private int doorAlignment;
    private Room destinationRoom;
    private World game;
    public ChangeRoomCommand(int doorAlignment, Room destinationRoom, World game)
    {
        this.doorAlignment = doorAlignment;
        this.destinationRoom = destinationRoom;
        this.game = game;
    }
    public void Execute()
    {
        game.SwitchRoom(doorAlignment, destinationRoom);
    }
}