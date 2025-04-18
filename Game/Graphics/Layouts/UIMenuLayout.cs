using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Game.Commands;
 
namespace Game.Graphics;

public class UIMenuLayout : UILayout {
    private UITextButton startButton;
    private UITextButton creditButton;
    public UIMenuLayout(GraphicsDevice device) : base() {
        int w = device.Viewport.Width;
        int h = device.Viewport.Height;

        Color buttonColor = Color.AntiqueWhite;

        // Adds Header Text
        Vector2 vec = new Vector2(w/2, h/2);
        vec.X -= Main.fonts["header"].MeasureString("Bombadeer Beetles").X/2;
        vec.Y = h/4;
        Color textColor = ColorTransform.Add(Color.AntiqueWhite, -30, -30, -30);
        UIText header = new UIText("Bombadeer Beetles", vec, "header", Color.Black);
        header.SetOutline(textColor);
        AddElement(header);

        // Adds Start Button
        Rectangle startButtonBounds = new Rectangle((int)(0.35*w), (int)(0.4*h),(int)(0.3*w), (int)(0.1*h));
        startButton = new UITextButton(startButtonBounds, Main.INSTANCE.mouse, new StartGameCommand(device, "game"), buttonColor, "Start", Color.Black, "arialbold");
        startButton.SetHoverColor(ColorTransform.Add(buttonColor, -20, -20, -20));
        AddElement(startButton);

        // Adds Credits Button
        Rectangle creditButtonBounds = new Rectangle((int)(0.35*w), (int)(0.55*h),(int)(0.3*w), (int)(0.1*h));
        creditButton = new UITextButton(creditButtonBounds, Main.INSTANCE.mouse, new CreditCommand(), buttonColor, "Credits", Color.Black, "arialbold");
        creditButton.SetHoverColor(ColorTransform.Add(buttonColor, -20, -20, -20));
        AddElement(creditButton);
    }
}