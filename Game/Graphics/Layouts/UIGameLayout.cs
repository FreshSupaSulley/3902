using System;
using Game.Rooms;
using Game.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Net.Mime;

namespace Game.Graphics;

public class UIGameLayout : UILayout {
    private bool variableTextAdded = false;
    private static readonly Sprite HUD = new(Main.Load("/Misc/HUD.png"), 2400, 600);
    private static readonly Sprite levelMap = new(Main.Load("/Misc/levelMap.png"));

    public UIGameLayout(GraphicsDevice device) {
        int w = device.Viewport.Width;
        int h = device.Viewport.Height;

        Color buttonColor = Color.AntiqueWhite;

        Rectangle pauseButtonBounds = new((int)(0.93*w), (int)(0.03*h),(int)(0.04*h), (int)(0.04*h));
        UISpriteButton pauseButton = new UISpriteButton(
            Main.uiManager.GetIcon("pause"),
            pauseButtonBounds,
            new PauseCommand()
        );
        pauseButton.SetHover(Main.uiManager.GetIcon("pauseHover"));
        AddElement(pauseButton);

    }
    public override void Update(GameTime gameTime) {
        if (!variableTextAdded && Main.INSTANCE.State is Game.State.Game) {
            Game.State.Game playerGame = (Game.State.Game) Main.INSTANCE.State;
            Func<int> function = playerGame.players[0].GetHealth;
            Rectangle bounds = new (600,80,100,30);
            UIHealthBar el = new UIHealthBar(function, bounds, new(95, 25), 0, 100);
            
            Func<int> key = playerGame.players[0].GetKey;
            Func<int> rupee = playerGame.players[0].GetRupee;
            Func<int> bomb = playerGame.players[0].GetBomb;
            UIVariableText<int> keyLayout = new UIKeyVariableText<int>(key, new Vector2(330,90), "arialbold", Color.White);
            UIVariableText<int> rupeeLayout = new UIKeyVariableText<int>(rupee, new Vector2(330, 45), "arialbold", Color.White);
            UIVariableText<int> bombLayout = new UIKeyVariableText<int>(bomb, new Vector2(330, 115), "arialbold", Color.White);
            keyLayout.SetOutline(Color.Black);
            AddElement(el);
            AddElement(keyLayout);
            AddElement(rupeeLayout);
            AddElement(bombLayout);
            variableTextAdded = true;

        }
        base.Update(gameTime);
    }
    public void RemoveHealth() {
        foreach(IUserInterfaceElement el in elements) {
            if (el is UIHealthVariableText<int>) {
                RemoveElement(el);
                break;
            }
        }
    }
    public override void Reset() {
        RemoveHealth();
        variableTextAdded = false;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        HUD.Draw(spriteBatch, new Vector2(0, 0));
        if (Room.currentRoom == "start")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(65, 39 * 0, 64, 39), 3.2f);
        }else if(Room.currentRoom == "keyRoom")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(0, 39 * 0, 64, 39), 3.2f);
        }else if(Room.currentRoom == "firstRight")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(129, 39 * 0, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "second")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(0, 39 * 1 + 1, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "thirdFirst")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(0, 39 * 2 + 1, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "thirdSecond")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(65, 39 * 2 + 1, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "thirdThird")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(129, 39 * 2 + 1, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "forthFirst")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(0, 39 * 3 + 1, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "forthSecond")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(64 * 1 + 1, 39 * 3 + 1, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "forthThird")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(64 * 2 + 1, 39 * 3 + 1, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "forthForth")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(64 * 3 + 1, 39 * 3 + 1, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "forthFifth")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(64 * 4 + 1, 39 * 3 + 1, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "fifthFirst")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(0, 39 * 4 + 1, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "fifthSecond")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(64 * 1 + 1, 39 * 4 + 1, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "fifthThird")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(64 * 2 + 1, 39 * 4 + 1, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "sixthFirst")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(0, 39 * 5 + 1, 64, 39), 3.2f);
        }
        else if (Room.currentRoom == "sixthSecond")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(65, 39 * 5 + 1, 64, 39), 3.2f);
        }
        base.Draw(spriteBatch);
    }
}