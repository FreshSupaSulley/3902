using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace Game.Controllers
{
    public class KeyboardController : IController
    {
        // Unused for now but a good value to have
        private Microsoft.Xna.Framework.Game game;
        private KeyboardState oldState;

        public KeyboardController(Microsoft.Xna.Framework.Game game)
        {
            this.game = game;
            PostUpdate();
        }

        // Unusued for now
        public void Update() {}

        public void PostUpdate()
        {
            oldState = Keyboard.GetState();
        }

        public bool IsKeyDown(params Keys[] keys) => keys.Any(key => Keyboard.GetState().IsKeyDown(key));
        public bool IsKeyPressed(params Keys[] keys) => keys.Any(key => Keyboard.GetState().IsKeyDown(key) && !oldState.IsKeyDown(key));
    }
}
