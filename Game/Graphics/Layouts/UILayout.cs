using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public abstract class UILayout : IUserInterfaceLayout {
    private List<IUserInterfaceElement> elements;
    public UILayout() {
        elements = new();
    }
    public void AddElement(IUserInterfaceElement el) {
        elements.Add(el);
    }
    public void Draw(SpriteBatch spriteBatch) {
        foreach (IUserInterfaceElement el in elements) {
            el.Draw(spriteBatch);
        }
    }
    public void Update(GameTime gameTime) {
        foreach (IUserInterfaceElement el in elements) {
            el.Update(gameTime);
        }
    }
}