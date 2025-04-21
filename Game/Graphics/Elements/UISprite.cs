using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

class UISprite : IUserInterfaceElement {
    private Texture2D sprite;
    private Rectangle bounds;
    private float rotation;
    private Vector2 origin;
    public UISprite(Texture2D texture, Rectangle bounds) : this(texture, texture.Bounds, bounds) {}
    public UISprite(Texture2D texture, Rectangle sourceRect, Rectangle bounds) {
        sprite = new(Main.INSTANCE.GraphicsDevice, sourceRect.Width, sourceRect.Height);
        Color[] data = new Color[sourceRect.Width*sourceRect.Height];
        texture.GetData(0,sourceRect,data, 0, sourceRect.Width*sourceRect.Height);
        sprite.SetData(data);
        this.bounds = bounds;
    }
    public void SetRotation(float rotation) {
        this.rotation = rotation;
    }
    public void SetBounds(Rectangle bounds) {
        this.bounds = bounds;
    }
    public void SetOrigin(Vector2 origin) {
        this.origin = origin;
    }
    public void SetSprite(Texture2D sprite) {
        this.sprite = sprite;
    }
    public void SetSprite(Texture2D texture, Rectangle sourceRect) {
        sprite = new(Main.INSTANCE.GraphicsDevice, sourceRect.Width, sourceRect.Height);
        Color[] data = new Color[sourceRect.Width*sourceRect.Height];
        texture.GetData(0,sourceRect,data, 0, sourceRect.Width*sourceRect.Height);
        sprite.SetData(data);
    }
    public void Update(GameTime gameTime) {}
    public void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(sprite, bounds,null, Color.White, rotation, origin, SpriteEffects.None, 1.0f);
    }
    
}