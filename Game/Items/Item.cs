using Game.Entities;
using Microsoft.Xna.Framework;

namespace Game.Items
{
    public abstract class Item(Vector2 position) : Entity(position)
    {
        /// Item was used
        public abstract void Use(Game game);
    }
}
