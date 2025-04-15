using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Commands;

namespace Game.Graphics;

public class UIWinLayout : UILayout {
    public UIWinLayout(GraphicsDevice device) {
        
        // Set basic parameters
        int w = device.Viewport.Width;
        int h = device.Viewport.Height;

        Color buttonColor = Color.AntiqueWhite;

        // Adds Header Text
        Vector2 vec = new Vector2(w/2, h/2);
        vec.X -= Main.fonts["header"].MeasureString("You Win!").X/2;
        vec.Y = h/4;
        UIText winHeader = new UIText("You Win!", vec, "header", Color.Yellow);
        winHeader.SetOutline(Color.Black);
        AddElement(winHeader);

        // Adds Play Again Button
        Rectangle playAgainButtonBounds = new Rectangle((int)(0.35*w), (int)(0.4*h),(int)(0.3*w), (int)(0.1*h));
        UITextButton playAgainButton = new UITextButton(playAgainButtonBounds, Main.INSTANCE.mouse, new StartGameCommand(device, "game"), buttonColor, "Play Again", Color.Black, "arialbold");
        playAgainButton.SetHoverColor(ColorTransform.Add(buttonColor, -20,-20,-20));
        AddElement(playAgainButton);

        // Adds Quit Button
        Rectangle quitButtonBounds = new Rectangle((int)(0.35*w), (int)(0.55*h),(int)(0.3*w), (int)(0.1*h));
        UITextButton quitButton = new UITextButton(quitButtonBounds, Main.INSTANCE.mouse, new QuitCommand(Main.INSTANCE), buttonColor, "Quit", Color.Black, "arialbold");
        quitButton.SetHoverColor(ColorTransform.Add(buttonColor, -20,-20,-20));
        AddElement(quitButton);
    }
}