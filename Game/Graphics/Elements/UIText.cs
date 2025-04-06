using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game;

namespace Game.Graphics;

public class UIText : IUserInterfaceElement {
    private Vector2 position;
    private string font;
    private Color textColor;
    protected string text;
    private Color outlineColor;
    private bool useOutline;

    public UIText(string text, Vector2 position, string font, Color color) {
        this.font = font;
        this.text = text;
        this.textColor = color;
        this.position = position;
    }
    public UIText(string text, Vector2 position) : this(text, position, null, new Color()) {}
    public virtual void Update(GameTime gameTime) {}
    public virtual void Draw(SpriteBatch sprites) {
        if (useOutline) {
            sprites.DrawString(Main.fonts[font], text, position+Vector2.One, outlineColor);
            sprites.DrawString(Main.fonts[font], text, position-Vector2.One, outlineColor);
            sprites.DrawString(Main.fonts[font], text, position+new Vector2(0,1), outlineColor);
            sprites.DrawString(Main.fonts[font], text, position-new Vector2(0,1), outlineColor);
            sprites.DrawString(Main.fonts[font], text, position+new Vector2(1,0), outlineColor);
            sprites.DrawString(Main.fonts[font], text, position-new Vector2(1,0), outlineColor);
        }

        sprites.DrawString(Main.fonts[font], text, position, textColor);
    }
    public void SetOutline(Color outlineColor) {
        this.outlineColor = outlineColor;
        useOutline = true;
    }
    public void ClearOutline() {
        useOutline = false;
    }
}