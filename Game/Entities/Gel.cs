using Game.Collision;
using Game.Graphics;
using Game.State;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    public class Gel : EnemyEntity
    {
        private static readonly Animation IDLE = new(Main.Load("/Entities/gel.png"), 2, 30);
        
        public Gel() : base(10,new Rectangle(0,0,10,10), IDLE) { }

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
