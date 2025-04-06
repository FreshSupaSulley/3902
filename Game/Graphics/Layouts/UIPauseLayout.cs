using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Commands;
using System;
namespace Game.Graphics;

public class UIPauseLayout : UILayout {
    private Rectangle resumeBounds;
    private bool changed;
    private UITextButton resumeButton;
    public UIPauseLayout(GraphicsDevice device) {
        int w = device.Viewport.Width;
        int h = device.Viewport.Height;
        resumeBounds = new Rectangle((int)(0.35*w), (int)(0.4*h),(int)(0.3*w), (int)(0.1*h));
        resumeButton = new UITextButton(resumeBounds, Main.INSTANCE.mouse, null, Color.AntiqueWhite, "Resume", Color.Black, "arialbold");
        AddElement(resumeButton);
        
        Vector2 vec = new Vector2(w/2, h/2);
        vec.X -= Main.fonts["header"].MeasureString("Paused").X/2;
        vec.Y = h/4;
        UIText el = new UIText("Paused", vec, "header", Color.AntiqueWhite);
        el.SetOutline(Color.Black);
        AddElement(el);

        changed = false;
    }
    
    public override void Update(GameTime gameTime)
    {
        if (Main.uiManager.Game != null && !changed) {
            resumeButton.UpdateCommand(new ResumeCommand(Main.uiManager.Game, "game"));
            changed = true;
        }
        base.Update(gameTime);
    }

    public override void Reset()
    {
        changed = false;
        base.Reset();
    }
}