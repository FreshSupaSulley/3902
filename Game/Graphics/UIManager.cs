using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public class UIManager {
    public string current;
    private Dictionary<string, IUserInterfaceLayout> uiDictionary;
    private Game game;
    public UIManager(Game game) {
        uiDictionary = new();
        this.game = game;
    }
    public void Update(GameTime gameTime) {
        uiDictionary[current].Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch) {
        uiDictionary[current].Draw(spriteBatch);
    }
    public void Load() {

    }
}