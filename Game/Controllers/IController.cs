using Microsoft.Xna.Framework;

namespace Game.Controllers;
public interface IController {
    void Update(GameTime gameTime);
    void PostUpdate();
}
