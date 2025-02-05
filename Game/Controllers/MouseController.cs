using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint0.Controllers;

public class MouseController : IController
{
    private Game game;
    private MouseState oldState;

    public MouseController(Game game)
    {
        this.game = game;
        PostTick();
    }

    // Unused for now
    public void Tick() {}

    public void PostTick()
    {
        oldState = Mouse.GetState();
    }

    public float RelativeX() => MathHelper.Clamp(Mouse.GetState().Position.X * 1f / game.Window.ClientBounds.Width, 0, 1);
    public float RelativeY() => MathHelper.Clamp(Mouse.GetState().Position.Y * 1f / game.Window.ClientBounds.Height, 0, 1);

    public bool LeftPressed() => Mouse.GetState().LeftButton == ButtonState.Pressed && oldState.LeftButton != ButtonState.Pressed;
    public bool RightPressed() => Mouse.GetState().RightButton == ButtonState.Pressed && oldState.RightButton != ButtonState.Pressed;

    public bool LeftDown() => Mouse.GetState().LeftButton == ButtonState.Pressed;
    public bool RightDown() => Mouse.GetState().RightButton == ButtonState.Pressed;
}
