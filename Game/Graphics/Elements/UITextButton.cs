using Microsoft.Xna.Framework;
using Game.Commands;
using Game.Controllers;
using Microsoft.Xna.Framework.Graphics;
using System.Globalization;

namespace Game.Graphics;

public class UITextButton : UIButton {
    public string text;
    private Vector2 textPos;
    private Color textColor;
    private string font;
    private UIText textEl;
    public UIText TextElement {get => textEl;}
    public UITextButton(Rectangle bounds, MouseController mc, ICommand onPress, Color color, string text, Color textColor, string font) : base (bounds, mc, onPress, color){
        this.font = font;
        this.text = text;
        Vector2 dim = Main.fonts[font].MeasureString(this.text);
        textPos = new Vector2();
        textPos.X = bounds.X+bounds.Width/2-dim.X/2;
        textPos.Y = bounds.Y+bounds.Height/2-dim.Y/2;
        this.textColor = textColor;
        textEl = new UIText(this.text, textPos, this.font, this.textColor);
    }
    public override void Draw(SpriteBatch spriteBatch) {
        base.Draw(spriteBatch);
        textEl.Draw(spriteBatch);
    }
    public void SetTextOutline(Color color) {
        textEl.SetOutline(color);
    }
}