using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Game.Commands;
 
namespace Game.Graphics;

public class MenuLayout : UILayout {
    public MenuLayout(GraphicsDevice device) : base() {
        int w = device.Viewport.Width;
        int h = device.Viewport.Height;
        Rectangle bounds = new Rectangle((int)(0.35*w), (int)(0.4*h),(int)(0.3*w), (int)(0.1*h));
        AddElement(new UITextButton(bounds, Main.INSTANCE.mouse, new StartGameCommand(device, "empty"), Color.AntiqueWhite, "Start", Color.Black, "arialbold"));
        Vector2 vec = new Vector2(w/2, h/2);
        vec.X -= Main.fonts["header"].MeasureString("Bombadeer Beetles").X/2;
        vec.Y = h/4;
        AddElement(new UIText("Bombadeer Beetles", vec, "header", Color.Black));
    }
}