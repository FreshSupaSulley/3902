namespace Game.Rooms
{
    public enum DoorType
    {
        OPEN, BREAK,

        // Cannot walk through them
        WALL = 32, LOCK, PUZZLE
    }
}
