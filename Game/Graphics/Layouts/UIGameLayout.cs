using System;
using Game.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class UIGameLayout : UILayout {
    private bool healthAdded = false;
    private static readonly Sprite HUD = new(Main.Load("/Misc/HUD.png"), 2400, 600);
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
            
            Func<int> key = playerGame.player.GetKey;
            Func<int> rupee = playerGame.player.GetRupee;
            Func<int> bomb = playerGame.player.GetBomb;
            UIVariableText<int> keyLayout = new UIKeyVariableText<int>(key, new Vector2(330,90), "arialbold", Color.White);
            UIVariableText<int> rupeeLayout = new UIKeyVariableText<int>(rupee, new Vector2(330, 45), "arialbold", Color.White);
            UIVariableText<int> bombLayout = new UIKeyVariableText<int>(bomb, new Vector2(330, 115), "arialbold", Color.White);
            keyLayout.SetOutline(Color.Black);
            AddElement(el);
            AddElement(keyLayout);
            AddElement(rupeeLayout);
            AddElement(bombLayout);
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

    public override void Draw(SpriteBatch spriteBatch)
    {
        HUD.Draw(spriteBatch, new Vector2(0, 0));
        base.Draw(spriteBatch);
    }
}