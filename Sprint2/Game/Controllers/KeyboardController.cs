using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace Game.Controllers
{
    public class KeyboardController : IController
    {
        // Unused for now but a good value to have
        private Game game;
        private static KeyboardState oldState;

        public KeyboardController(Game game)
        {
            this.game = game;
            PostUpdate();
        }

        // Unusued for now
        public void Update() {}

        public void PostUpdate()
        {
            KeyboardController.oldState = Keyboard.GetState();
        }

        public static bool IsKeyDown(params Keys[] keys) => keys.Any(key => Keyboard.GetState().IsKeyDown(key));
        public static bool IsKeyPressed(params Keys[] keys) => keys.Any(key => Keyboard.GetState().IsKeyDown(key) && !KeyboardController.oldState.IsKeyDown(key));
    }
}
