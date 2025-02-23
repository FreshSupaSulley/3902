using Game.Entities;
using Microsoft.Xna.Framework;

namespace Game.Items
{
    public abstract class Item(Vector2 position) : Entity(position)
    {
        /// Item was used
        public virtual void Use(Game game)
        {
            // game.AddGameObject(this);
        }
    }
}
