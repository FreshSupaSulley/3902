using Game.Collision;
using Game.Graphics;
using Game.State;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq.Expressions;

namespace Game.Entities
{
    public class Skeleton : EnemyEntity
    {
        private static readonly Animation IDLE = new(Main.Load("/Entities/skeleton.png"), 2, 30);

        public Skeleton() : base(10,new Rectangle(0, 0, 16, 16), IDLE)
        {

        }

        public override Vector2 Move(State.Game game)
        {
            Vector2 direction = game.player.Position - Position;

            if (direction != Vector2.Zero)
                direction.Normalize();

            // speed
            float speed = 0.5f;

            // Move toward player
            return direction * speed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
