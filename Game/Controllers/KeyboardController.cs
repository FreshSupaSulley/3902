using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint0.Controllers;

public class KeyboardController : IController
{
    // Unused for now but a good value to have
    private Game game;
    private KeyboardState oldState;

    public KeyboardController(Game game)
    {
        this.game = game;
        PostTick();
    }

    // Unusued for now
    public void Tick() {}

    public void PostTick()
    {
        oldState = Keyboard.GetState();
    }

    public bool IsKeyDown(params Keys[] keys) => keys.Any(key => Keyboard.GetState().IsKeyDown(key));
    public bool IsKeyPressed(params Keys[] keys) => keys.Any(key => Keyboard.GetState().IsKeyDown(key) && !oldState.IsKeyDown(key));
}
