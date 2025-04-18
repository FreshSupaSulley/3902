using Game.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.State;
class Credits : IGameState {
    public void Update(GameTime gameTime) {
        Main.uiManager.Update(gameTime);
    }
    public void Draw(SpriteBatch batch) {
        batch.Begin();
        Main.uiManager.Draw(batch);
        batch.End();
    }
}