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
        if (!variableTextAdded && Main.INSTANCE.State is State.Game) {
            Game.State.Game playerGame = (Game.State.Game) Main.INSTANCE.State;

            Rectangle bounds1 = new (600,80,100,20);
            Func<int> function1 = playerGame.players[0].GetHealth;
            UIHealthBar player1Health = new UIHealthBar(function1, bounds1, new(95, 15), 0, 100);

            Rectangle bounds2 = new (600,105,100,20);
            Func<int> function2 = playerGame.players[1].GetHealth;
            UIHealthBar player2Health = new UIHealthBar(function2, bounds2, new(95, 15), 0, 100);
            
            AddElement(player1Health);
            AddElement(player2Health);

            Func<int> key = playerGame.players[0].GetKey;
            Func<int> rupee = playerGame.players[0].GetRupee;
            Func<int> bomb = playerGame.players[0].GetBomb;
            UIVariableText<int> keyLayout = new UIKeyVariableText<int>(key, new Vector2(330,90), "arialbold", Color.White);
            UIVariableText<int> rupeeLayout = new UIKeyVariableText<int>(rupee, new Vector2(330, 45), "arialbold", Color.White);
            UIVariableText<int> bombLayout = new UIKeyVariableText<int>(bomb, new Vector2(330, 115), "arialbold", Color.White);
            keyLayout.SetOutline(Color.Black);
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
        int textureHeight = 40;
        int textureWidth = 64;
        HUD.Draw(spriteBatch, new Vector2(0, 0));
        if (Room.currentRoom == "start")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 1, textureHeight * 0, 64, textureHeight), 3.2f);
        }else if(Room.currentRoom == "keyRoom")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 0, textureHeight * 0, 64, textureHeight), 3.2f);
        }else if(Room.currentRoom == "firstRight")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 2, textureHeight * 0, 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "second")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 0, textureHeight * 1 , 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "thirdFirst")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 0, textureHeight * 2 , 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "thirdSecond")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 1, textureHeight * 2 , 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "thirdThird")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 2, textureHeight * 2 , 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "forthFirst")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 0, textureHeight * 3 , 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "forthSecond")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 1, textureHeight * 3 , 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "forthThird")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 2, textureHeight * 3 , 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "forthForth")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 3, textureHeight * 3 , 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "forthFifth")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 4, textureHeight * 3 , 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "fifthFirst")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 0, textureHeight * 4 , 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "fifthSecond")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 1, textureHeight * 4 , 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "fifthThird")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 2, textureHeight * 4 , 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "sixthFirst")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 0, textureHeight * 5 , 64, textureHeight), 3.2f);
        }
        else if (Room.currentRoom == "sixthSecond")
        {
            levelMap.Draw(spriteBatch, new Vector2(48, 23), new Rectangle(textureWidth * 1, textureHeight * 5 , 64, textureHeight), 3.2f);
        }
        base.Draw(spriteBatch);
    }
}