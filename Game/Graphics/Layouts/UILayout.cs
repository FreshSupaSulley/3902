using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game.Graphics;

public abstract class UILayout : IUserInterfaceLayout {
    protected List<IUserInterfaceElement> elements;
    public UILayout() {
        elements = new();
    }
    public void AddElement(IUserInterfaceElement el) {
        elements.Add(el);
    }
    public void RemoveElement(IUserInterfaceElement el) {
        elements.Remove(el);
    }
    public virtual void Draw(SpriteBatch spriteBatch) {
        foreach (IUserInterfaceElement el in elements) {
            el.Draw(spriteBatch);
        }
    }
    public virtual void Update(GameTime gameTime) {
        foreach (IUserInterfaceElement el in elements) {
            el.Update(gameTime);
        }
    }
    public virtual void Reset() {}
    public virtual void ClearElements() {
        elements.Clear();
    }
}