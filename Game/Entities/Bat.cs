using Game.Graphics;
using Game.Path;
using Game.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    public class Bat : LivingEntity
    {
        private static readonly Animation IDLE = new(Main.Load("/Entities/bat.png"), 2, 30);

        public Bat() : base(new Rectangle(0, 0, 14, 14), IDLE)
        {
        }

        public override Vector2 Move(State.Game game)
        {
            Vector2 direction = game.player.Position - Position;

            if (direction != Vector2.Zero)
                direction.Normalize();

            float speed = 0.5f;

            return direction * speed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
