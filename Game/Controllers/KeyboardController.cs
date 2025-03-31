using System.Collections.Generic;
using System.Linq;
using Game.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game.Controllers;

public class KeyboardController : IController
{
    private KeyboardState oldState;
    private KeyboardState state;

    private readonly Dictionary<Keys, ICommand> onCommand;
    private readonly Dictionary<Keys, ICommand> onKeyup;
    private readonly Dictionary<Keys, ICommand> onKeydown;
    private readonly Dictionary<Keys, ICommand> onHold;
    private readonly Dictionary<Keys, float> holdTimes;
    private readonly Dictionary<Keys, float> elapsedTimes;

    public KeyboardController()
    {
        onCommand = new Dictionary<Keys, ICommand>();
        onKeyup = new Dictionary<Keys, ICommand>();
        onKeydown = new Dictionary<Keys, ICommand>();
        onHold = new Dictionary<Keys, ICommand>();
        holdTimes = new Dictionary<Keys, float>();
        elapsedTimes = new Dictionary<Keys, float>();

        oldState = Keyboard.GetState();
        state = Keyboard.GetState();
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

    public void AddHoldCommand(Keys key, ICommand command, float holdTime) {
        if (onHold.ContainsKey(key))
        {
            onHold.Remove(key);
        }
        onHold.Add(key, command);
        if (holdTimes.ContainsKey(key))
        {
            holdTimes.Remove(key);
        }
        holdTimes.Add(key, holdTime);
        if (elapsedTimes.ContainsKey(key))
        {
            elapsedTimes.Remove(key);
        }
        elapsedTimes.Add(key, 0);
    }
    public void AddKeydownCommand(Keys key, ICommand command) {
        if (onKeydown.ContainsKey(key))
        {
            onKeydown.Remove(key);
        }
        onKeydown.Add(key, command);
    }
    public void AddKeyupCommand(Keys key, ICommand command) {
        if (onKeyup.ContainsKey(key))
        {
            onKeyup.Remove(key);
        }
        onKeyup.Add(key, command);
    }
    public void RemoveHoldCommand(Keys key) {
        if (onHold.ContainsKey(key))
        {
            onHold.Remove(key);
        }
        if (holdTimes.ContainsKey(key))
        {
            holdTimes.Remove(key);
        }
        if (elapsedTimes.ContainsKey(key))
        {
            elapsedTimes.Remove(key);
        }
    }
    public void RemoveKeydownCommand(Keys key) {
        if (onKeydown.ContainsKey(key))
        {
            onKeydown.Remove(key);
        }
    }
    public void RemoveKeyupCommand(Keys key) {
        if (onKeyup.ContainsKey(key))
        {
            onKeyup.Remove(key);
        }
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

        foreach (KeyValuePair<Keys, ICommand> pair in onCommand) {
            if (state.IsKeyDown(pair.Key)) {
                pair.Value.Execute();
            }
        }
        foreach (KeyValuePair<Keys, ICommand> pair in onKeydown) {
            if (state.IsKeyDown(pair.Key) && oldState.IsKeyUp(pair.Key)) {
                pair.Value.Execute();
            }
        }
        foreach (KeyValuePair<Keys, ICommand> pair in onKeyup) {
            if (state.IsKeyUp(pair.Key) && oldState.IsKeyDown(pair.Key)) {
                pair.Value.Execute();
            }
        }
        foreach (KeyValuePair<Keys, ICommand> pair in onHold) {
            if (state.IsKeyDown(pair.Key)) {
                elapsedTimes[pair.Key] += (float) gameTime.ElapsedGameTime.TotalSeconds;
            } else {
                elapsedTimes[pair.Key] = 0;
            }
            if (elapsedTimes[pair.Key] > holdTimes[pair.Key]) {
                pair.Value.Execute();
                elapsedTimes[pair.Key] = 0;
            }
        }

    }

    public void PostUpdate()
    {
        oldState = state;
    }

    public bool IsKeyDown(params Keys[] keys) => keys.Any(state.IsKeyDown);
    public bool IsKeyPressed(params Keys[] keys) => keys.Any(key => state.IsKeyDown(key) && !oldState.IsKeyDown(key));
}
