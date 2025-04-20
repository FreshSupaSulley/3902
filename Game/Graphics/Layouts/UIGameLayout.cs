using System;
using Game.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class UIGameLayout : UILayout {
    private bool healthAdded = false;
    public UIGameLayout(GraphicsDevice device) {
        int w = device.Viewport.Width;
        int h = device.Viewport.Height;

        // UITextButton pauseButton = new UITextButton(bounds, Main.INSTANCE.mouse, new PauseCommand, buttonColor, "⏸︎", Color.Black, "arialbold");

    }
    public override void Update(GameTime gameTime) {
        if (!healthAdded && Main.INSTANCE.State is Game.State.Game) {
            Game.State.Game playerGame = (Game.State.Game) Main.INSTANCE.State;
            Func<int> function = playerGame.players[0].GetHealth;
            Rectangle bounds = new (50,20,100,30);
            UIHealthBar el = new UIHealthBar(function, bounds, new(95, 25), 0, 100);
            
<<<<<<< HEAD
            Func<int> key = playerGame.players[0].GetKey;
            UIVariableText<int> keyLayout = new UIKeyVariableText<int>(key, new Vector2(180,20), "arialbold", Color.White);
=======
            Func<int> key = playerGame.player.GetKey;
            UIVariableText<int> keyLayout = new UIKeyVariableText<int>(key, new Vector2(340,90), "arialbold", Color.White);
>>>>>>> 44c33fbeede618f7afbffce30791bd5e2749e86c
            keyLayout.SetOutline(Color.Black);

            AddElement(el);
            AddElement(keyLayout);
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