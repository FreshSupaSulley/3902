using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game.Graphics;

public class UIVariableText<T> : UIText {
    protected Func<T> getValue;
    public UIVariableText(Func<T> getValue, Vector2 position, string font, Color color) : base("", position, font, color) {
        this.getValue = getValue;
        ConvertVariableToText();
    }
    public UIVariableText(Func<T> getValue, Vector2 position): this(getValue, position, null, new Color()) {}
    public virtual string ConvertVariableToText() {
        text = getValue()?.ToString();
        return text;
    }
    public override void Update(GameTime gameTime) {
        ConvertVariableToText();
    }
}