using System.Collections.Generic;
using System.Linq;
using Game.Commands;
using Microsoft.Xna.Framework.Input;

namespace Game.Controllers;

public class KeyboardController : IController {
    // Unused for now but a good value to have
    private Microsoft.Xna.Framework.Game game;
    private KeyboardState oldState;
    private KeyboardState state;

    private Dictionary<Keys, ICommand> commandDictionary;

    public KeyboardController(Game game)
    {
        this.game = game;
        commandDictionary = new Dictionary<Keys, ICommand>();
        PostUpdate();
    }

    public void AddCommand(Keys key, ICommand command) {
        if (commandDictionary.ContainsKey(key)) {
            commandDictionary.Remove(key);
        }
        commandDictionary.Add(key, command);
    }
    public void AddCommand(Dictionary<Keys, ICommand> newCommandDictionary) {
        foreach (KeyValuePair<Keys, ICommand> pair in newCommandDictionary) {
            AddCommand(pair.Key, pair.Value);
        }
    }
    public void RemoveCommand(Keys key) {
        if (commandDictionary.ContainsKey(key)) {
			commandDictionary.Remove(key);
		}
    }
    public void RemoveCommand(Dictionary<Keys, ICommand> newCommandDictionary) {
        foreach (KeyValuePair<Keys, ICommand> pair in newCommandDictionary) {
            RemoveCommand(pair.Key);
        }
    }

    // Unusued for now
    public void Update() {
        state = Keyboard.GetState();

        foreach (Keys key in state.GetPressedKeys()) {
            if (commandDictionary.ContainsKey(key)) {
                commandDictionary[key].Execute();
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
