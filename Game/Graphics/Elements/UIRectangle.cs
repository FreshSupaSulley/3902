using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

class UIRectangle : IUserInterfaceElement {
    Texture2D pixel;
    Rectangle bounds;
    Color color;
    public UIRectangle(Rectangle bounds, Color color) {
        pixel = new Texture2D(Main.INSTANCE.GraphicsDevice, 1, 1);
        pixel.SetData(new [] {color});
        this.bounds = bounds;
        this.color = color;
    }
    public void Update(GameTime gameTime) {}
    public void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(pixel, bounds, color);
    }
}