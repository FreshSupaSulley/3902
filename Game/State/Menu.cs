using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Game.Util;
using Game.Controllers;

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
            if (Main.INSTANCE.mouse.LeftDown())
            {
                Main.SwitchGameState(new World(device));
            }
        }

        // Render
        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            FontRenderer.Text("hi!!!!", batch, new(100, 100));
            batch.End();
        }
    }
}
