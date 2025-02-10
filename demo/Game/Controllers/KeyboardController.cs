using System;
using System.Collections.Generic;
using System.Linq;
using demo.Game.Commands;
using Microsoft.Xna.Framework.Input;

namespace Game.Controllers
{
    public class KeyboardController : IController
    {
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
    }
}
