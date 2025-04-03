using Microsoft.Xna.Framework;
using Game.Commands;
using Game.Controllers;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public class UITextButton : UIButton {
    public string text;
    private Vector2 textPos;
    private Color textColor;
    public UITextButton(Rectangle bounds, MouseController mc, ICommand onPress, Color color, string text, Color textColor) : base (bounds, mc, onPress, color){

        this.text = text;
        Vector2 dim = Main.INSTANCE.font.MeasureString(this.text);
        textPos = new Vector2();
        textPos.X = bounds.X+bounds.Width/2-dim.X/2;
        textPos.Y = bounds.Y+bounds.Height/2-dim.Y/2;
        this.textColor = textColor;
    }
    public override void Draw(SpriteBatch spriteBatch) {
        base.Draw(spriteBatch);
        spriteBatch.DrawString(Main.INSTANCE.font, text, textPos, textColor);
    }
}