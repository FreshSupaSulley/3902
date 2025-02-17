using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    public class Bomb : Item
    {
        private static readonly Sprite BOMB = new(Game.Load("/Items/zelda_items.png", new(136, 0, 8, 14)));

        // Fire has an animation not a sprite
        private static Texture2D FIRE_TEX = Game.Load("/Misc/fire.png");
        private Animation FIRE = new(FIRE_TEX, 2, 10);

        private int ticks;
        private readonly int bombDelay = 60;

        public bool isExploded()
        {
            return ticks > bombDelay;
        }

        public override void Update()
        {
            if (!isExploded())
            {
                ticks++;
            }
            else
            {
                FIRE.Update();
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            if (!isExploded())
            {
                BOMB.Draw(batch, Position);
            }
            else
            {
                FIRE.Draw(batch, Position);
            }
        }
    }
}
