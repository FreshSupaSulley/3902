using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Commands;

namespace Game.Graphics;

public class UIPauseLayout : UILayout {
    private bool changed;
    private UITextButton resumeButton;
    public UIPauseLayout(GraphicsDevice device) {
        int w = device.Viewport.Width;
        int h = device.Viewport.Height;

        Color buttonColor = Color.AntiqueWhite;

        // Darken Background
        Color backgroundColor = new Color(0,0,0,150);
        Rectangle backgroundBounds = new Rectangle(0,0,w,h);
        UIRectangle backgroundRect = new UIRectangle(backgroundBounds, backgroundColor);
        AddElement(backgroundRect);

        // Adds Header Text
        Vector2 vec = new Vector2(w/2, h/2);
        vec.X -= Main.fonts["header"].MeasureString("Paused").X/2;
        vec.Y = h/4;
        UIText el = new UIText("Paused", vec, "header", Color.AntiqueWhite);
        el.SetOutline(Color.Black);
        AddElement(el);

        // Adds Resume Button
        Rectangle resumeBounds = new Rectangle((int)(0.35*w), (int)(0.4*h),(int)(0.3*w), (int)(0.1*h));
        resumeButton = new UITextButton(resumeBounds, Main.INSTANCE.mouse, null, buttonColor, "Resume", Color.Black, "arialbold");
        resumeButton.SetHoverColor(ColorTransform.Add(buttonColor, -20,-20,-20));
        AddElement(resumeButton);
        
        // Adds Quit Button
        Rectangle quitButtonBounds = new Rectangle((int)(0.35*w), (int)(0.55*h),(int)(0.3*w), (int)(0.1*h));
        UITextButton quitButton = new UITextButton(quitButtonBounds, Main.INSTANCE.mouse, new QuitCommand(Main.INSTANCE), buttonColor, "Quit", Color.Black, "arialbold");
        quitButton.SetHoverColor(ColorTransform.Add(buttonColor, -20,-20,-20));
        AddElement(quitButton);

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