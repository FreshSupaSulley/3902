using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

class UIRectangle : IUserInterfaceElement {
    private Texture2D pixel;
    private Rectangle bounds;
    private Color color;
    private float rotation = 0;
    private Vector2 origin;
    public UIRectangle(Rectangle bounds, Color color) {
        pixel = new Texture2D(Main.INSTANCE.GraphicsDevice, 1, 1);
        pixel.SetData(new [] {color});
        this.bounds = bounds;
        this.color = color;
        origin = new Vector2(
            0.5f, 0.5f
        );
    }
    public void SetBounds(Rectangle bounds) {
        this.bounds = bounds;
    }
    public void SetColor(Color color) {
        this.color = color;
    }
    public void SetRotation (float rotation) {
        this.rotation = rotation;
    }
    public void SetOrigin (Vector2 origin) {
        this.origin = origin;
    }
    public void Update(GameTime gameTime) {}
    public void Draw(SpriteBatch spriteBatch) {

        Vector2 position = new Vector2(bounds.X + bounds.Width / 2f, bounds.Y + bounds.Height / 2f);
        Vector2 scale = new Vector2(bounds.Width, bounds.Height);

        spriteBatch.Draw(
            pixel,
            position,
            null,
            color,
            rotation,
            origin,
            scale,
            SpriteEffects.None,
            0f
        );
    }
}