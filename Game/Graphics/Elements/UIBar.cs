using System;
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
    protected UIRectangle backgroundRect;
    protected UIRectangle foregroundRect;
    protected Color internalColor;
    protected Color externalColor;
    public UIBar(Func<int> getValue, Rectangle bounds, Vector2 barDimensions, int minValue, int maxValue, Color internalColor, Color externalColor) {
        this.getValue = getValue;
        this.bounds = bounds;
        this.barDimensions = barDimensions;
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.internalColor = internalColor;
        this.externalColor = externalColor;

        foregroundRect = new(new (0,0,0,(int)barDimensions.Y), internalColor);
        backgroundRect = new(bounds, externalColor);
    }
    public UIBar(Func<int> getValue, Rectangle bounds, Vector2 barDimensions, int minValue, int maxValue) : this(getValue, bounds, barDimensions, minValue, maxValue, Color.White, Color.Gray) {}
    public virtual void Update(GameTime gameTime) {
        previousValue = currentValue;
        currentValue = getValue();

        if (currentValue != previousValue) {
            if (currentValue < previousValue) {
                OnDecrease();
            } else if (currentValue > previousValue) {
                OnIncrease();
            }
            Rectangle newBounds = new(
                (int)(bounds.X + bounds.Width/2 - barDimensions.X/2),
                (int)(bounds.Y + bounds.Height/2 - barDimensions.Y/2),
                (int) (barDimensions.X * ((float)currentValue - minValue)/((float)maxValue-minValue)),
                (int) barDimensions.Y
            );
            Vector2 newOrigin = new(
                0.5f,
                0.5f
            );
            foregroundRect.SetOrigin(newOrigin);
            foregroundRect.SetBounds(newBounds);
        }

    }
    public void SetInternalColor(Color color) {
        internalColor = color;
        foregroundRect = new(new (0,0,0,(int)barDimensions.Y), color);
    }
    public void SetExternalColor(Color color) {
        externalColor = color;
        backgroundRect = new(bounds, color);
    }
    public virtual void OnDecrease() {}
    public virtual void OnIncrease() {}
    public void Draw(SpriteBatch spriteBatch) {
        backgroundRect.Draw(spriteBatch);
        foregroundRect.Draw(spriteBatch);
    }
}