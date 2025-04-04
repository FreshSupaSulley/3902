using Microsoft.Xna.Framework;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    public class Fireball(Vector2 position) : Entity(position)
    {
        private static readonly Sprite SPRITE = new Sprite(Main.Load("/Entities/Dragon/projectile.png"));
        private static readonly float speed = 1f;
        private static readonly int timeAlive = 30;

        // Used to track when it should despawn
        private int ticksAlive;

        public override void Update(State.Game game)
        {
            Position -= new Vector2(speed, 0);
            // Add hitbox to room
            game.room.AddHitbox(new(10, this, new(0, 0, SPRITE.Texture.Width, SPRITE.Texture.Height)));
            // Check whether to remove it from the game
            if (ticksAlive++ > timeAlive)
            {
                game.room.RemoveEntity(this);
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            SPRITE.Draw(batch, Position);
        }
    }
}
