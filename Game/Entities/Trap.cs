using Game.Collision;
using Game.Graphics;
using Game.State;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    public class Trap : LivingEntity
    {
        private static readonly Animation IDLE = new(Main.Load("/Entities/trap.png"), 1, 20);

        public Trap() : base (new Rectangle(0, 0, 16, 16), IDLE)
        {

        }

        public override Vector2 Move(State.Game game)
        {
            Vector2 toPlayer = game.player.Position - Position;
            float speed = 1f;

            if(game.player.Position.X == Position.X)
            {
                return new Vector2(toPlayer.X, 0) * speed;
            }

            return Vector2.Zero;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
