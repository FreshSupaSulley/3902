using Game.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    public abstract class Item : IGameObject
    {
        public Vector2 Position;

        /// Item was used
        public virtual void Use(Game game)
        {
            game.AddGameObject(this);
        }

        // Pass to subclass
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update();
    }
}
