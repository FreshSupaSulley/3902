using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public class UIManager {
    public string current;
    private Dictionary<string, IUserInterfaceLayout> uiDictionary;
    public UIManager() {
        uiDictionary = new();
        current = Main.startingUI;
    }
    public void Update(GameTime gameTime) {
        uiDictionary[current].Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch) {
        uiDictionary[current].Draw(spriteBatch);
    }
    public void ChangeUIState(string newValue) {
        current = newValue;
    }
    public void Load() {
        uiDictionary.Add("empty", new EmptyLayout());
        uiDictionary.Add("menu", new MenuLayout(Main.INSTANCE.spriteBatch.GraphicsDevice));
    }
}