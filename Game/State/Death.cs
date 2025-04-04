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
            if (Main.INSTANCE.mouse.LeftDown())
            {
                Main.SwitchGameState(new Game(device));
            }
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            FontRenderer.Text("you fucking died!! click to restart :)))", batch, new(100, 100));
            batch.End();
        }
    }
}
