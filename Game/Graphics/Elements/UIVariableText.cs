using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game.Graphics;

public class UIVariableText<T> : UIText {
    private readonly Func<T> getValue;
    public UIVariableText(Func<T> getValue, Vector2 position, SpriteFont font, Color color) : base("", position, font, color) {
        this.getValue = getValue;
        ConvertVariableToText();
    }
    public UIVariableText(Func<T> getValue, Vector2 position): this(getValue, position, null, new Color()) {}
    public string ConvertVariableToText() {
        text = getValue()?.ToString();
        return text;
    }
    override public void Update(GameTime gameTime) {
        ConvertVariableToText();
    }
}