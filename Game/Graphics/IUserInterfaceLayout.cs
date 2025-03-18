using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public interface IUserInterfaceLayout {
    public void AddElement(IUserInterfaceElement el);
    public void Draw(SpriteBatch spriteBatch);
}