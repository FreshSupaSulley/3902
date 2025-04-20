using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;

namespace Game.Graphics;

class UIHealthBar : UIBar {
    private float currentRotation = 0.0f;
    private float maxRotation = MathF.PI/8;
    private float minRotation = -MathF.PI/8;
    private float restingRotation = 0.0f;
    private bool animation = false;
    private int direction = -1;
    private float speed = 2*MathF.PI;
    private int passes = 0;
    public UIHealthBar(Func<int> getValue, Rectangle bounds, Vector2 barDimensions, int minValue, int maxValue): base(getValue, bounds, barDimensions, minValue, maxValue) {}
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (animation) {
            float t = (float) gameTime.ElapsedGameTime.TotalSeconds;
            float previousRotation = currentRotation;
            currentRotation += t * speed * direction;
            if (currentRotation > maxRotation) {
                float difference = currentRotation - maxRotation;
                currentRotation -= difference;
                direction *= -1;
            } else if (currentRotation < minRotation) {
                float difference = minRotation - currentRotation;
                currentRotation += difference;
                direction *= -1;
            }
            if (
                (previousRotation > restingRotation && currentRotation < restingRotation) ||
                (previousRotation < restingRotation && currentRotation > restingRotation)
            ) {
                passes++;
                if (passes >= 3) {
                    currentRotation = restingRotation;
                    animation = false;
                }
            }
        }
        foregroundRect.SetRotation(currentRotation);
        backgroundRect.SetRotation(currentRotation);
    }
    public override void OnDecrease()
    {
        base.OnDecrease();
        animation = true;
        currentRotation = 0.0f;
        passes = 0;

    }
    public override void OnIncrease()
    {
        base.OnIncrease();
    }
}