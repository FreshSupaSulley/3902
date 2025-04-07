using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game.Graphics;

public class UIKeyVariableText<T> : UIVariableText<T> {
    public UIKeyVariableText(Func<T> getValue, Vector2 position, string font, Color color) : base(getValue, position, font, color) {}
    public override string ConvertVariableToText() {
        text = "Key: "+getValue()?.ToString();
        return text;
    }
    public override void Draw(SpriteBatch spriteBatch) {
        base.Draw(spriteBatch);
    }
}