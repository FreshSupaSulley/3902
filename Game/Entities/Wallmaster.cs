using Game.Collision;
using Game.Graphics;
using Game.State;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    public class Wallmaster : LivingEntity
    {
        private static readonly Animation IDLE = new(Main.Load("/Entities/wallmaster.png"), 2, 50);

        public Wallmaster() : base(10,new Rectangle(0, 0, 16, 16), IDLE)
        {

        }

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
