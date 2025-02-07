using System.Numerics;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game.Controllers;

namespace Game.Entities
{
    // An Entity is an object in the game with a position
    public class DemoPlayer : LivingEntity
    {
        private int speed = 2;

        public DemoPlayer(Vector2 position) : base(position, AnimationRegistry.Run)
        {
        }

        public override void Update()
        {
            base.Update();

            Position += GetVelocity();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        private Vector2 GetVelocity() {
            Vector2 velocity = new Vector2();
            if(KeyboardController.IsKeyDown(Keys.W)) {
                velocity += new Vector2(0, -speed);
            }
            if(KeyboardController.IsKeyDown(Keys.S)) {
                velocity += new Vector2(0, speed);
            }
            if(KeyboardController.IsKeyDown(Keys.A)) {
                velocity += new Vector2(-speed, 0);
            }
            if(KeyboardController.IsKeyDown(Keys.D)) {
                velocity += new Vector2(speed, 0);
            }
            return velocity;
        }
    }
}
