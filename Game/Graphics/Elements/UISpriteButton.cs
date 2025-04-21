using System.Runtime.CompilerServices;
using Game.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

class UISpriteButton : UIButton {
    public UISprite sprite;
    public UISprite spriteHover;
    private Rectangle bounds;
    public UISpriteButton(Texture2D texture, Rectangle sourceRect, Rectangle bounds, ICommand command) : base(bounds, Main.INSTANCE.mouse, command) {
        this.bounds = bounds;
        spriteHover = null;
        sprite = new UISprite(texture, sourceRect, bounds);
    }
    public UISpriteButton(Texture2D texture, Rectangle bounds, ICommand command) : this(texture, texture.Bounds, bounds, command) {}
    public void SetHover(Texture2D texture, Rectangle sourceRect) {
        spriteHover = new UISprite(texture, sourceRect, bounds);
    }
    public void SetHover(Texture2D texture) {
        SetHover(texture, texture.Bounds);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        if (hovered && spriteHover != null) {
            spriteHover.Draw(spriteBatch);
        } else {
            sprite.Draw(spriteBatch);
        }
    }
}