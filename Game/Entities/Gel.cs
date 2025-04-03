using Game.Collision;
using Game.Graphics;
using Game.State;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    public class Gel() : LivingEntity(new(0, 0, 10, 10), IDLE)
    {
        private static readonly Animation IDLE = new(Main.Load("/Entities/gel.png"), 2, 20);

        public override Vector2 Move(State.Game game)
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
