using Microsoft.Xna.Framework;
using Game.Controllers;
using Game.Commands;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game.Graphics;

public class UIButton : IUserInterfaceElement {
    private Rectangle bounds;
    private MouseController mc;
    private ICommand onPress;
    private bool pressed = false;
    protected Color color;
    protected Texture2D pixel;
    public UIButton(Rectangle bounds, MouseController mc, ICommand onPress, Color color) {
        this.bounds = bounds;
        this.mc = mc;
        this.onPress = onPress;
        this.color = color;

        pixel = new Texture2D(Main.INSTANCE.GraphicsDevice, 1, 1);
        pixel.SetData(new [] {color});

    }
    public UIButton(Rectangle bounds, MouseController mc, ICommand onPress) : this(bounds, mc, onPress, Color.Black) {}
    public virtual void Update(GameTime gameTime) {
        if (
            bounds.Left <= mc.PositionX() && mc.PositionX() <= bounds.Right &&
            bounds.Top <= mc.PositionY() && mc.PositionY() <= bounds.Bottom && mc.LeftDown() && !pressed
        ) {
            onPress.Execute();
        }
        if (!mc.LeftDown()) {
            pressed = false;
        }
    }
    public virtual void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(pixel, bounds, color);
        Console.WriteLine(bounds);
    }
}