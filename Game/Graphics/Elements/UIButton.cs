using Microsoft.Xna.Framework;
using Game.Controllers;
using Game.Commands;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;

namespace Game.Graphics;

public class UIButton : IUserInterfaceElement {
    private Rectangle bounds;
    private MouseController mc;
    private ICommand onPress;
    private bool pressed = true;
    protected Color color;
    protected Color hoverColor;
    protected bool hovered;
    protected Texture2D pixel;
    protected int borderWidth;
    protected Color borderColor;
    private bool cursorSet;
    public UIButton(Rectangle bounds, MouseController mc, ICommand onPress, Color color) {
        this.bounds = bounds;
        this.mc = mc;
        this.onPress = onPress;
        this.color = color;
        this.hoverColor = color;

        pressed = true;

        pixel = new Texture2D(Main.INSTANCE.GraphicsDevice, 1, 1);
        pixel.SetData(new [] {color});

    }
    public UIButton(Rectangle bounds, MouseController mc, ICommand onPress) : this(bounds, mc, onPress, Color.Black) {}
    public virtual void Update(GameTime gameTime) {
        
        if (
            bounds.Left <= mc.PositionX() && mc.PositionX() <= bounds.Right &&
            bounds.Top <= mc.PositionY() && mc.PositionY() <= bounds.Bottom
        ) {
            hovered = true;
            if (mc.LeftDown() && !pressed) {
                onPress.Execute();
            }
        } else {
            hovered = false;
        }
        if (!mc.LeftDown()) {
            pressed = false;
        } else {
            pressed = true;
        }
    }
    public void UpdateCommand(ICommand command) {
        onPress = command;
    }
    public virtual void Draw(SpriteBatch spriteBatch) {
        Rectangle borderBounds = new Rectangle(
            bounds.X - borderWidth,
            bounds.Y - borderWidth,
            bounds.Width + 2*borderWidth,
            bounds.Height + 2*borderWidth
        );
        spriteBatch.Draw(pixel, borderBounds, borderColor);
        if (hovered) {
            spriteBatch.Draw(pixel, bounds, hoverColor);
            if (!cursorSet) {
                Mouse.SetCursor(MouseCursor.Hand);
                cursorSet = true;
            }
        } else {
            if (cursorSet) {
                Mouse.SetCursor(MouseCursor.Arrow);
                cursorSet = false;
            }
            spriteBatch.Draw(pixel, bounds, color);
        }
    }
    public void SetHoverColor(Color hoverColor) {
        this.hoverColor = hoverColor;
    }
    public void SetBorder(Color borderColor, int borderWidth) {
        this.borderColor = borderColor;
        this.borderWidth = borderWidth;
    }
    public void SetBorder(Color borderColor) {
        SetBorder(borderColor, 1);
    }
    public void Reset() {
        // pressed = true;
    }
}