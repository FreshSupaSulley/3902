using Game.Collision;
using Game.Graphics;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    public class Bat(Vector2 Position) : LivingEntity(new StillCollisionBox(0,0,10, 10, null), Position, IDLE)
    {
        private static readonly Animation IDLE = new(Game.Load("/Entities/bat.png"), 2, 20);

        private int ticks;

        public override Vector2 Move(Game game)
        {
            return new Vector2(0, 0);
            //return new Vector2(0, ticks / 10 % 2 == 0 ? 1 : -1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
