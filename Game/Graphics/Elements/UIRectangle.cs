using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

class UIRectangle : IUserInterfaceElement {
    private Texture2D pixel;
    private Rectangle bounds;
    private Color color;
    private float rotation;
    public UIRectangle(Rectangle bounds, Color color) {
        pixel = new Texture2D(Main.INSTANCE.GraphicsDevice, 1, 1);
        pixel.SetData(new [] {color});
        this.bounds = bounds;
        this.color = color;
    }
    public void UpdateBounds(Rectangle bounds) {
        this.bounds = bounds;
    }
    public void UpdateColor(Color color) {
        this.color = color;
    }
    public void UpdateRotation (float rotation) {
        this.rotation = rotation;
    }
    public void Update(GameTime gameTime) {}
    public void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(
            pixel, 
            bounds, 
            new Rectangle(0,0,1,1), 
            color, 
            rotation, 
            new Vector2(bounds.Width/2, bounds.Height/2), 
            SpriteEffects.None, 
            1.0f
        );
    }
}