using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Util;

namespace Game.State
{
    public class Win : IGameState
    {
        public void Update(GameTime gameTime)
        {
            if (Main.INSTANCE.mouse.LeftDown())
            {
                Main.SwitchGameState(new Game());
            }
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            Main.uiManager.Draw(batch);
            batch.End();
        }
    }
}
