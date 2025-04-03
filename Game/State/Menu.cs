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
        protected UIManager uiManager;
        
        public Menu(GraphicsDevice device)
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
            FontRenderer.Text("crude home menu for functionality check (left click the screen)!!!!", batch, new(100, 100));
            batch.End();
        }
    }
}
