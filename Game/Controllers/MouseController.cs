using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game.Controllers
{
    public class MouseController : IController
    {
        private MouseState oldState;
        private MouseState state;

        // Unused for now
        public void Update(GameTime gameTime)
        {
            state = Mouse.GetState();
        }

        public void PostUpdate()
        {
            oldState = state;
        }

        public float RelativeX() => MathHelper.Clamp(this.state.Position.X * 1f / Main.INSTANCE.Window.ClientBounds.Width, 0, 1);
        public float RelativeY() => MathHelper.Clamp(this.state.Position.Y * 1f / Main.INSTANCE.Window.ClientBounds.Height, 0, 1);

        public int PositionX() => this.state.Position.X;
        public int PositionY() => this.state.Position.Y;

        public bool LeftPressed() => this.state.LeftButton == ButtonState.Pressed && oldState.LeftButton != ButtonState.Pressed;
        public bool RightPressed() => this.state.RightButton == ButtonState.Pressed && oldState.RightButton != ButtonState.Pressed;

        public bool LeftDown() => this.state.LeftButton == ButtonState.Pressed;
        public bool RightDown() => this.state.RightButton == ButtonState.Pressed;
    }
}
