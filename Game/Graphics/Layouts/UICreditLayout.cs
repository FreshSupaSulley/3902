using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game.Commands;
using Game.State;
using System.Collections.Generic;

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

        // Add Names
        string[] names = ["Erich Sullivan Boschert","Mingyu Choi","Ty Tram Fredrick","Jared Willets"];
        int c = 0;
        foreach (string name in names) {
            Vector2 namePos = new(
                w/2 - Main.fonts["header"].MeasureString(name).X/2,
                h/4 + 48*(c+1) + 24
            );
            UIText currentName = new UIText(name, namePos, "header", Color.AntiqueWhite);
            currentName.SetOutline(Color.Black);
            AddElement(currentName);
            c++;
        }

        // Add Back Button
        Rectangle backButtonBounds = new((int)(0.05*w), (int)(0.9*h),(int)(0.1*w), (int)(0.05*h));
        UITextButton backButton = new UITextButton(backButtonBounds, Main.INSTANCE.mouse, new MenuCommand(), buttonColor, "Back", Color.Black, "arialbold");
        backButton.SetHoverColor(ColorTransform.Add(buttonColor, -20,-20,-20));
        AddElement(backButton);
    }
}