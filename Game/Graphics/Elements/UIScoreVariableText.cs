using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game.Graphics;

public class UIHealthVariableText<T> : UIVariableText<T> {
    public UIHealthVariableText(Func<T> getValue, Vector2 position, string font, Color color) : base(getValue, position, font, color) {}
    public override string ConvertVariableToText() {
        text = "Health: "+getValue()?.ToString();
        return text;
    }
    public override void Draw(SpriteBatch spriteBatch) {
        base.Draw(spriteBatch);
    }
}