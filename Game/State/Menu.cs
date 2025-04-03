using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Game.Util;
using Game.Controllers;
using Game.Graphics;

namespace Game.State
{
    public class Menu : IGameState
    {
        private GraphicsDevice device;
        
        public Menu(GraphicsDevice device)
        {
            this.device = device;
        }
        
        public void Update(GameTime gameTime)
        {
            // if (Main.INSTANCE.mouse.LeftDown())
            // {
            //     Main.SwitchGameState(new Game(device));
            // }
            Main.uiManager.Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            Main.uiManager.Draw(batch);
            FontRenderer.Text("crude home menu for functionality check (left click the screen)!!!!", batch, new(100, 100));
            batch.End();
        }
    }
}
