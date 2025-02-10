<<<<<<< HEAD
using System;
=======
>>>>>>> 8a1e0fdc5b5b986c53e508c9fdac8eeb69e57db1
using System.Collections.Generic;
using System.Linq;
using demo.Game.Commands;
using Microsoft.Xna.Framework.Input;
using Game.Commands;

namespace Game.Controllers;

public class KeyboardController : IController {
    // Unused for now but a good value to have
    private Microsoft.Xna.Framework.Game game;
    private KeyboardState oldState;
    private KeyboardState state;

    private Dictionary<Keys, ICommand> commandDictionary;

    public KeyboardController(Game game)
    {
<<<<<<< HEAD
        // Unused for now but a good value to have
        private Microsoft.Xna.Framework.Game game;
        Dictionary<Keys, ICommand> mappings;
        private KeyboardState oldState;

        public KeyboardController(Microsoft.Xna.Framework.Game game)
        {
            this.game = game;
            PostUpdate();
        }

        // Unusued for now
        public void Update(Object cntrlrStt) {
            if(cntrlrStt is KeyboardState)
            {
                KeyboardState keyS = (KeyboardState)cntrlrStt;
                Keys[] pressed = keyS.GetPressedKeys();
                for(int i = 0; i < pressed.Length; i++)
                {
                    if (mappings.ContainsKey(pressed[i]))
                    {
                        mappings[pressed[i]].Execute();
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: KeyBoardController cannot react with parameter of type {cntrlrStt.GetType}\n");
            }
        }

        public void PostUpdate()
        {
            oldState = Keyboard.GetState();
        }

        public void map(Dictionary<Keys, ICommand> m)
        {
            mappings = m;
        }

        public bool IsKeyDown(params Keys[] keys) => keys.Any(key => Keyboard.GetState().IsKeyDown(key));
        public bool IsKeyPressed(params Keys[] keys) => keys.Any(key => Keyboard.GetState().IsKeyDown(key) && !oldState.IsKeyDown(key));
=======
        this.game = game;
        commandDictionary = new Dictionary<Keys, ICommand>();
        PostUpdate();
>>>>>>> 8a1e0fdc5b5b986c53e508c9fdac8eeb69e57db1
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
