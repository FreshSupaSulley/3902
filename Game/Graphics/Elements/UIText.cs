using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game;

namespace Game.Graphics;

public class UIText : IUserInterfaceElement {
    private Vector2 position;
    private string font;
    private Color textColor;
    protected string text;

    public UIText(string text, Vector2 position, string font, Color color) {
        this.font = font;
        this.text = text;
        this.textColor = color;
        this.position = position;
    }
    public UIText(string text, Vector2 position) : this(text, position, null, new Color()) {}
    public virtual void Update(GameTime gameTime) {}
    public virtual void Draw(SpriteBatch sprites) {
        sprites.DrawString(Main.fonts[font], text, position, textColor);
    }
}