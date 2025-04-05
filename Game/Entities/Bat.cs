using Game.Graphics;
using Game.Path;
using Game.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    public class Bat() : LivingEntity(new(0, 0, 10, 10), IDLE)
    {
        private static readonly Animation IDLE = new(Main.Load("/Entities/bat.png"), 2, 20);

        private static readonly IPath[] motionPaths = [
            new LinearPath(new Vector2(200,200), new Vector2(100,0), 50),
            new LinearPath(new Vector2(300,200), new Vector2(-100,0), 50),
            new LinearPath(new Vector2(200,200), new Vector2(0,100), 50),
            new LinearPath(new Vector2(200,300), new Vector2(0,-200), 100),
            new LinearPath(new Vector2(200,100), new Vector2(0,100), 50),
            new LinearPath(new Vector2(200,200), new Vector2(-100,0), 50),
            new LinearPath(new Vector2(100,200), new Vector2(100,0), 50),
        ];

        public override Vector2 Move(State.Game game)
        {
            // Get player's position
            Vector2 playerPos = game.player.Position;

            // Calculate direction vector toward player
            Vector2 direction = playerPos - Position;

            // Normalize direction to get unit vector (keeps movement consistent)
            if (direction != Vector2.Zero)
                direction.Normalize();

            // Bat speed (adjust as needed)
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
