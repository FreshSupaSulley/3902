using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Util;
using Microsoft.Xna.Framework.Input;

namespace Game.State
{
    public class Pause : IGameState
    {
        private Game game;

        public Pause(Game game)
        {
            this.game = game;
        }

        public void Update(GameTime gameTime)
        {
            if (Main.INSTANCE.keyboard.IsKeyPressed(Keys.Escape))
            {
                Main.SwitchGameState(game);
                return;
            }
            Main.uiManager.Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            // Draw screen underneath
            game.Draw(batch);
            // Now draw pause on top
            batch.Begin();
            batch.End();
        }
    }
}
