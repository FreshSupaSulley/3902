using Game.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    public class Boomerang(Vector2 position) : Item(position)
    {
        private static readonly Sprite SPRITE = new(Game.Load("/Items/zelda_items.png", new(129, 3, 5, 8)));

        private readonly int TICKS_ALIVE = 500;
        private int ticks = 0;

        public override void Update(Game game)
        {
            Position += new Vector2(1, 0);

            if(ticks++ > TICKS_ALIVE)
            {
                // call a despawn, which I dont care enough to do rn
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(SPRITE.Texture, Position, null, Color.White, MathHelper.ToRadians(ticks * 4), new Vector2(5f / 2, 8f / 2), new Vector2(1), SpriteEffects.None, 0f);
        }
    }
}
