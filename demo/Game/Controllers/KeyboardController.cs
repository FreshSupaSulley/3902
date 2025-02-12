using System.Collections.Generic;
using System.Linq;
using Game.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game.Controllers;

public class KeyboardController : IController
{
    // Unused for now but a good value to have
    private readonly Microsoft.Xna.Framework.Game game;
    private KeyboardState oldState;
    private KeyboardState state;

    private readonly Dictionary<Keys, ICommand> onCommand;
    private readonly Dictionary<Keys, ICommand> onKeyup;
    private readonly Dictionary<Keys, ICommand> onKeydown;
    private readonly Dictionary<Keys, ICommand> onHold;
    private readonly Dictionary<Keys, float> holdTimes;

    public KeyboardController(Game game)
    {
        this.game = game;
        onCommand = new Dictionary<Keys, ICommand>();
        onKeyup = new Dictionary<Keys, ICommand>();
        onKeydown = new Dictionary<Keys, ICommand>();
        onHold = new Dictionary<Keys, ICommand>();
        holdTimes = new Dictionary<Keys, float>();
    }

    public void AddCommand(Keys key, ICommand command)
    {
        if (onCommand.ContainsKey(key))
        {
            onCommand.Remove(key);
        }
        onCommand.Add(key, command);
    }

    public void AddCommand(Dictionary<Keys, ICommand> newCommandDictionary)
    {
        foreach (KeyValuePair<Keys, ICommand> pair in newCommandDictionary)
        {
            AddCommand(pair.Key, pair.Value);
        }
    }

    public void AddTapCommand() {

    }

    public void RemoveCommand(Keys key)
    {
        if (onCommand.ContainsKey(key))
        {
            onCommand.Remove(key);
        }
    }

    public void RemoveCommand(Dictionary<Keys, ICommand> newCommandDictionary)
    {
        foreach (KeyValuePair<Keys, ICommand> pair in newCommandDictionary)
        {
            RemoveCommand(pair.Key);
        }
    }

    public void Update(GameTime gameTime)
    {
        state = Keyboard.GetState();

        foreach (Keys key in state.GetPressedKeys())
        {
            if (onCommand.ContainsKey(key))
            {
                onCommand[key].Execute();
            }
        }
    }

    public void PostUpdate()
    {
        oldState = state;
    }

    public bool IsKeyDown(params Keys[] keys) => keys.Any(key => Keyboard.GetState().IsKeyDown(key));
    public bool IsKeyPressed(params Keys[] keys) => keys.Any(key => Keyboard.GetState().IsKeyDown(key) && !oldState.IsKeyDown(key));
}
