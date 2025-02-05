using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    // IGameObject have Update and Draw methods.
    public interface IGameObject
    {
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
