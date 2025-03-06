using System.Runtime.CompilerServices;
using Game.Commands;
using Game.Entities;
using Game.Rooms;
using Microsoft.Xna.Framework;

namespace Game.Commands;

class ChangeRoomCommand : ICommand {
    /**
        doorAlignment: 0 for left, 1 for top, 2 for right, 3 for bottom
    */
    private int doorAlignment;
    private Room destinationRoom;
    private Game game;
    private Player player;
    ChangeRoomCommand(int doorAlignment, Room destinationRoom, Game game, Player player) {
        this.doorAlignment = doorAlignment;
        this.destinationRoom = destinationRoom;
        this.game = game;
        this.player = player;
    }
    public void Execute() {
        game.room = destinationRoom;
        switch (doorAlignment) {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
}