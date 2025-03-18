using Microsoft.Xna.Framework;
using Game.Controllers;
using Game.Commands;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public class UIButton : IUserInterfaceElement {
    private Rectangle bounds;
    private MouseController mc;
    private ICommand onPress;
    private bool pressed = false;
    public UIButton(Rectangle bounds, MouseController mc, ICommand onPress) {
        this.bounds = bounds;
        this.mc = mc;
        this.onPress = onPress;
    }
    public void Update(GameTime gameTime) {
        if (
            bounds.Left <= mc.PositionX() && mc.PositionX() <= bounds.Right &&
            bounds.Top <= mc.PositionY() && mc.PositionY() <= bounds.Bottom && mc.LeftDown() && !pressed
        ) {
            onPress.Execute();
        }
        if (!mc.LeftDown()) {
            pressed = false;
        }
    }
    public void Draw(SpriteBatch spriteBatch) {
        
    }
}