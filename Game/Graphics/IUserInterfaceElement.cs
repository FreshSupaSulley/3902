using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public interface IUserInterfaceElement {
    public void Update(GameTime gameTime);
    public void Draw(SpriteBatch spriteBatch);
}