using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game.Graphics;

public class UIKeyVariableText<T> : UIVariableText<T> {

    private static readonly Sprite HUD = new(Main.Load("/Misc/HUD.png"),2400,600);
    public UIKeyVariableText(Func<T> getValue, Vector2 position, string font, Color color) : base(getValue, position, font, color) {}
    public override string ConvertVariableToText() {
        text = ""+getValue()?.ToString();
        return text;
    }
    public override void Draw(SpriteBatch spriteBatch) {
        HUD.Draw(spriteBatch, new Vector2(0,0));
        base.Draw(spriteBatch);
    }
}