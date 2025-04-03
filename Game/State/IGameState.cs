using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.State
{
    public interface IGameState
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch batch);

        // These are optional
        public void OnEnter() { }
        public void OnExit() { }
    }
}
