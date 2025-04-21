using System;
using Game.Rooms;
using Game.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Graphics;

public class UIGameLayout : UILayout {
    private bool variableTextAdded = false;
    private static readonly Sprite HUD = new(Main.Load("/Misc/HUD.png"), 2400, 600);
    private static readonly Sprite levelMap = new(Main.Load("/Misc/levelMap.png"));
    private Vector2 mapSpritePos = new Vector2(0, 0);
    private Rectangle sourceRectangle;

    public UIGameLayout(GraphicsDevice device) {
        int w = device.Viewport.Width;
        int h = device.Viewport.Height;

        Color buttonColor = Color.AntiqueWhite;

        Rectangle pauseButtonBounds = new((int)(0.75*w), (int)(0.05*h),(int)(0.15*w), (int)(0.05*h));
        UITextButton pauseButton = new UITextButton(
            pauseButtonBounds, 
            Main.INSTANCE.mouse, 
            new PauseCommand(), 
            buttonColor, 
            "Pause",
            Color.Black, 
            "arialbold"
        );
        pauseButton.SetHoverColor(ColorTransform.Add(Color.AntiqueWhite, -30));
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

            
            if(Room.currentRoom == "start")
            {
                sourceRectangle = new Rectangle(65, 0, 64, 40);
            }else if(Room.currentRoom == "keyRoom")
            {
                sourceRectangle = new Rectangle(0, 0, 64, 40);
            }else if(Room.currentRoom == "firstRight")
            {
                sourceRectangle = new Rectangle(129, 0, 64, 40);
            }
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
        levelMap.Draw(spriteBatch, new Vector2(48, 23), sourceRectangle, 3.2f);
        base.Draw(spriteBatch);
    }
}