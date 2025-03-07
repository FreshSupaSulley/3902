using System.Runtime.CompilerServices;
using Game.Commands;
using Game.Entities;
using Game.Rooms;
using Microsoft.Xna.Framework;

namespace Game.Commands;

class ChangeRoomCommand : ICommand {
    private int doorAlignment;
    private Room destinationRoom;
    private Game game;
    private Player player;
    public ChangeRoomCommand(int doorAlignment, Room destinationRoom, Game game) {
        this.doorAlignment = doorAlignment;
        this.destinationRoom = destinationRoom;
        this.game = game;
    }
    public void Execute() {
        game.SwitchRoom(doorAlignment, destinationRoom);
    }
}