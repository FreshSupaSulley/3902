using System.Collections.Generic;
using Game.Graphics;
using Game.Path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{

    public class Gohma : MobileMotionPathEntity {
        private static readonly Texture2D spriteSheet = Game.Load("/Dragon/zelda_Bosses.png");
        private static readonly Rectangle[] rects = new Rectangle[] {
            new Rectangle(298, 105, 48, 16),
            new Rectangle(347, 105, 48, 16),
            new Rectangle(396, 105, 48, 16),
            new Rectangle(445, 105, 48, 16),
        };
        private static readonly Animation IDLE = new Animation(spriteSheet, rects);
        
        public Dictionary<int, Animation> animationSequences { get; set; }
        private Animation prev;
        private static readonly IPath[] motionPaths = {
            new LinearPath(new Vector2(200,200), new Vector2(100,0), 50),
            new LinearPath(new Vector2(300,200), new Vector2(-100,0), 50),
            new LinearPath(new Vector2(200,200), new Vector2(0,100), 50),
            new LinearPath(new Vector2(200,300), new Vector2(0,-200), 100),
            new LinearPath(new Vector2(200,100), new Vector2(0,100), 50),
            new LinearPath(new Vector2(200,200), new Vector2(-100,0), 50),
            new LinearPath(new Vector2(100,200), new Vector2(100,0), 50),
        };

        public Gohma(): base(new Vector2(200, 200), IDLE, motionPaths) {
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
}