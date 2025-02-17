using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    // Hearts dont do anything yet
    public class Heart : Item
    {
        private static readonly Sprite SPRITE = new(Game.Load("/Items/zelda_items.png", new(0, 0, 7, 8)));

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch batch)
        {
            SPRITE.Draw(batch, Position);
        }
    }
}
