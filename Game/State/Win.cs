using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Util;

namespace Game.State
{
    public class Win : IGameState
    {
        public void Update(GameTime gameTime)
        {
            Main.uiManager.Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            Main.uiManager.Draw(batch);
            batch.End();
        }
    }
}
