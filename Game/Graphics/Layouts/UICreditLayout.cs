using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public class UICreditLayout : UILayout { 
    public UICreditLayout(GraphicsDevice device) {
        int w = device.Viewport.Width;
        int h = device.Viewport.Height;

        Color buttonColor = Color.AntiqueWhite;

        // Adds Header Text
        string headerText = "Credits";
        Vector2 vec = new Vector2(w/2, h/2);
        vec.X -= Main.fonts["header"].MeasureString(headerText).X/2;
        vec.Y = h/4;
        Color textColor = ColorTransform.Add(Color.AntiqueWhite, -30, -30, -30);
        UIText header = new UIText(headerText, vec, "header", Color.Black);
        header.SetOutline(textColor);
        AddElement(header);

        // Add Back Button
        
    }
}