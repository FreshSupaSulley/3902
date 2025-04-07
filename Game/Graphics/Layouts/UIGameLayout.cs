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
            UIVariableText<int> el = new UIHealthVariableText<int>(function, position, "arialbold", Color.White);
            el.SetOutline(Color.Black);
            AddElement(el);
            healthAdded = true;
        }
        base.Update(gameTime);
    }
    public void RemoveHealth() {
        foreach(IUserInterfaceElement el in elements) {
            if (el is UIHealthVariableText<int>) {
                RemoveElement(el);
                break;
            }
        }
    }
    public override void Reset() {
        RemoveHealth();
        healthAdded = false;
    }
}