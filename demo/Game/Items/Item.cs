using System.Numerics;
using Game.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    public abstract class IItem : IGameObject
    {
        public Vector2 Position;

        /// Item was used
        public abstract void Use();

        // Pass to subclass
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update();
    }
}
