using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Game.Util;
using Game.Controllers;

namespace Game.State
{
    public class Death : IGameState
    {
        private GraphicsDevice device;

        public Death(GraphicsDevice device)
        {
            this.device = device;
        }

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
