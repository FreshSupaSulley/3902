using Microsoft.Xna.Framework;
using Game.Graphics;
using Game.Entities;
using Microsoft.Xna.Framework.Graphics;
using Game.Collision;
using Game.Commands;
using Game.State;

namespace Game.Tiles
{
    public class Fireball : Entity
    {
        private static readonly Sprite dragon_projectile;
        private static readonly float speed = 1f;
        private static readonly int timeAlive = 30;
        private CollisionBox collisionBox;

        // Used to track when it should despawn
        private int ticksAlive;

        static Fireball() {
            dragon_projectile = new Sprite(Main.Load("/Entities/Dragon/projectile.png"));
        }

        public Fireball(Vector2 position) : base(position) {
            Rectangle bounds = new Rectangle((int)position.X, (int)position.Y, 10, 10); // Change to size of fireball if incorrect
            collisionBox = new PushCollisionBox(bounds, new DamageCommand(Game.State.Game.instance.player, 20));
        }

        public override void Update(State.Game game)
        {
            Position -= new Vector2(speed, 0);
            if(ticksAlive++ > timeAlive)
            {
                game.room.RemoveEntity(this);
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            dragon_projectile.Draw(batch, Position);
        }
    }
}
