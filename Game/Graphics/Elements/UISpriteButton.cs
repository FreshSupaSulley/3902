using System.Runtime.CompilerServices;
using Game.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

class UISpriteButton : UIButton {
    public UISprite sprite;
    public UISpriteButton(Texture2D texture, Rectangle sourceRect, Rectangle bounds, ICommand command) : base(bounds, Main.INSTANCE.mouse, command) {
        sprite = new UISprite(texture, sourceRect, bounds);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        sprite.Draw(spriteBatch);
    }
}