using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

class UIBar : IUserInterfaceElement {
    protected Func<int> getValue;
    protected Rectangle bounds;
    protected Vector2 barDimensions;
    protected int minValue;
    protected int maxValue;
    protected int previousValue;
    protected int currentValue;
    UIRectangle backgroundRect;
    UIRectangle foregroundRect;
    public UIBar(Func<int> getValue, Rectangle bounds, Vector2 barDimensions, int minValue, int maxValue) {
        this.getValue = getValue;
        this.bounds = bounds;
        this.barDimensions = barDimensions;
        this.minValue = minValue;
        this.maxValue = maxValue;
    }
    public void Update(GameTime gameTime) {
        previousValue = currentValue;
        currentValue = getValue();
        if (currentValue < previousValue) {
            OnDecrease();
        } else if (currentValue > previousValue) {
            OnIncrease();
        }

        Rectangle newBounds = new(
            (int)(bounds.X + bounds.Width/2 - barDimensions.X/2),
            (int)(bounds.Y + bounds.Height/2 - barDimensions.Y/2),
            (int) (barDimensions.X * (currentValue - minValue)/(maxValue-minValue)),
            (int) barDimensions.Y
        );

        foregroundRect.UpdateBounds(newBounds);
    }
    public virtual void OnDecrease() {}
    public virtual void OnIncrease() {}
    public void Draw(SpriteBatch spriteBatch) {
        backgroundRect.Draw(spriteBatch);
        foregroundRect.Draw(spriteBatch);
    }
}