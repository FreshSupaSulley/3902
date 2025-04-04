using System;
using Game.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class UIGameLayout : UILayout {
    private bool healthAdded = false;
    public UIGameLayout(GraphicsDevice device) {
        int w = device.Viewport.Width;
        int h = device.Viewport.Height;
    }
    public override void Update(GameTime gameTime) {
        if (!healthAdded && Main.INSTANCE.State is Game.State.Game) {
            Game.State.Game playerGame = (Game.State.Game) Main.INSTANCE.State;
            Func<int> function = playerGame.player.GetHealth;
            Vector2 position = new Vector2(50,20);
            AddElement(new UIHealthVariableText<int>(function, position, "arialbold", Color.White));
            healthAdded = true;
        }
        base.Update(gameTime);
    }
    public void Reset() {
        
    }
}