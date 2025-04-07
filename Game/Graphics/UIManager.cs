using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public class UIManager {
    public string current;
    private State.Game game = null;

    public State.Game Game {get {return game;}}

    private Dictionary<string, IUserInterfaceLayout> uiDictionary;
    public UIManager() {
        uiDictionary = new();
        current = Main.startingUI;
    }
    public void SetGame(State.Game game) {
        this.game = game;
    }
    public void ClearGame() {
        game = null;
    }
    public void Update(GameTime gameTime) {
        uiDictionary[current].Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch) {
        uiDictionary[current].Draw(spriteBatch);
    }
    public void ChangeUIState(string newValue) {
        current = newValue;
        uiDictionary[current].Reset();
    }
    public void Load() {
        uiDictionary.Add("empty", new UIEmptyLayout());
        uiDictionary.Add("menu", new UIMenuLayout(Main.INSTANCE.spriteBatch.GraphicsDevice));
        uiDictionary.Add("game", new UIGameLayout(Main.INSTANCE.spriteBatch.GraphicsDevice));
        uiDictionary.Add("death", new UIDeathLayout(Main.INSTANCE.spriteBatch.GraphicsDevice));
        uiDictionary.Add("win", new UIWinLayout(Main.INSTANCE.spriteBatch.GraphicsDevice));
        uiDictionary.Add("pause", new UIPauseLayout(Main.INSTANCE.spriteBatch.GraphicsDevice));
    }
    public void AddElement(IUserInterfaceElement el) {
        uiDictionary[current].AddElement(el);
    }
    public void Reset() {
    }
}