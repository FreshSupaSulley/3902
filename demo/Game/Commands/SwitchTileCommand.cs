using System;
using Game.Entities;
using Game.Items;
using Game.Tiles;

namespace Game.Commands
{
    internal class SwitchTileCommand(Tile tile, bool forwards) : ICommand
    {
        public void Execute()
        {
            TileType[] values = (TileType[])Enum.GetValues(typeof(TileType));
            int index = Array.IndexOf(values, tile.Type);
            index = (index + (forwards ? 1 : values.Length - 1)) % values.Length;
            tile.Type = values[index];
        }
    }
}
