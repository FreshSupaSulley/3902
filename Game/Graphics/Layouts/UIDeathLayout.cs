using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Commands;

namespace Game.Graphics;

public class UIDeathLayout : UILayout {
    public UIDeathLayout(GraphicsDevice device) {
        int w = device.Viewport.Width;
        int h = device.Viewport.Height;
        Rectangle bounds = new Rectangle((int)(0.35*w), (int)(0.4*h),(int)(0.3*w), (int)(0.1*h));
        AddElement(new UITextButton(bounds, Main.INSTANCE.mouse, new StartGameCommand(device, "game"), Color.AntiqueWhite, "Respawn", Color.Black, "arialbold"));
        Vector2 vec = new Vector2(w/2, h/2);
        vec.X -= Main.fonts["header"].MeasureString("You DIED!").X/2;
        vec.Y = h/4;
        AddElement(new UIText("You DIED!", vec, "header", Color.Black));
    }
}