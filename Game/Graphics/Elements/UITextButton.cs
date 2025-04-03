using Microsoft.Xna.Framework;
using Game.Commands;
using Game.Controllers;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public class UITextButton : UIButton {
    public string text;
    public UITextButton(Rectangle bounds, MouseController mc, ICommand onPress, string text) : base(bounds, mc, onPress){
        this.text = text;
    }
    public override void Draw(SpriteBatch spriteBatch) {
        base.Draw(spriteBatch);
        
    }
}