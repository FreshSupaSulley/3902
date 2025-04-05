using Game.Graphics;
using Game.State;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    public class Dragon : LivingEntity
    {
        private static readonly Animation IDLE = new(Main.Load("/Entities/Dragon/dragon.png"), 4, 10);
        private static readonly Animation HURT = new(Main.Load("/Entities/Dragon/damaged.png"), 1, 1);

        public Dragon(): base(new Rectangle(0, 0, 16, 16), IDLE)
        {

        }

        private int ticks;

        public override Vector2 Move(State.Game game)
        {
            if (ticks++ % 60 == 0)
            {
                game.room.AddEntity(new Fireball(Position));
            }
            return new Vector2(0, ticks / 10 % 2 == 0 ? 1 : -1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
