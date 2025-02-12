using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game.Controllers
{
    public class MouseController : IController
    {
        private Microsoft.Xna.Framework.Game game;
        private MouseState oldState;
        private MouseState state;

        public MouseController(Microsoft.Xna.Framework.Game game)
        {
            this.game = game;
            Update();
            PostUpdate();
        }

        // Unused for now
        public void Update() {
            state = Mouse.GetState();
        }

        public void PostUpdate()
        {
            oldState = state;
        }

        public float RelativeX() => MathHelper.Clamp(Mouse.GetState().Position.X * 1f / game.Window.ClientBounds.Width, 0, 1);
        public float RelativeY() => MathHelper.Clamp(Mouse.GetState().Position.Y * 1f / game.Window.ClientBounds.Height, 0, 1);

        public bool LeftPressed() => Mouse.GetState().LeftButton == ButtonState.Pressed && oldState.LeftButton != ButtonState.Pressed;
        public bool RightPressed() => Mouse.GetState().RightButton == ButtonState.Pressed && oldState.RightButton != ButtonState.Pressed;

        public bool LeftDown() => Mouse.GetState().LeftButton == ButtonState.Pressed;
        public bool RightDown() => Mouse.GetState().RightButton == ButtonState.Pressed;
    }
}
