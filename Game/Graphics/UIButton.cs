using Microsoft.Xna.Framework;
using Game.Controllers;
using Game.Commands;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public class UIButton : IUserInterfaceElement {
    private Rectangle bounds;
    private MouseController mc;
    private ICommand onPress;
    public UIButton(Rectangle bounds, MouseController mc, ICommand onPress) {
        this.bounds = bounds;
        this.mc = mc;
        this.onPress = onPress;
    }
    public void Update(GameTime gameTime) {}
    public void Draw(SpriteBatch spriteBatch) {

    }
}