using System.Collections.Generic;
using Game.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities;

public class Gleeok : MobileEntity {
    private static readonly Texture2D spriteSheet = Game.Load("/Dragon/zelda_Bosses.png");
    private static readonly Animation IDLE = new Animation(spriteSheet, rects);
    private static Rectangle[] rects = new Rectangle[] {
        new Rectangle(96, 11, 219, 42),
        new Rectangle(220, 11, 243, 42),
        new Rectangle(244, 11, 267, 42),
    };
    
    public Dictionary<int, Animation> animationSequences { get; set; }
    private Animation prev;

    Gleeok(): base(new Vector2(200, 200), IDLE) {
        animationSequences = new Dictionary<int, Animation>();
        animationSequences.Add(0, IDLE);
    }
    public void animate(int i) {
        prev = this.activeAnimation;
        this.activeAnimation = animationSequences[i];
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}