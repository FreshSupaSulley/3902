using System;
using Microsoft.Xna.Framework;

namespace Game.Graphics;

class UIHealthBar : UIBar {
    public UIHealthBar(Func<int> getValue, Rectangle bounds, Vector2 barDimensions, int minValue, int maxValue): base(getValue, bounds, barDimensions, minValue, maxValue) {}
}