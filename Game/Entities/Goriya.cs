using Game.Graphics;
using Game.Path;
using Game.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game.Entities
{
    public class Goriya : LivingEntity
    {

        private static readonly Texture2D GORIYA_TEXTURE = Main.Load("/Entities/goriya.png");

        private static readonly Animation DOWN = new(Main.Load("/Entities/goriya.png", new Rectangle(0, 0, 32, 16)), 2, 20);
        private static readonly Animation UP = new(Main.Load("/Entities/goriya.png", new Rectangle(32, 0, 32, 16)), 2, 20);
        private static readonly Animation RIGHT = new(Main.Load("/Entities/goriya.png", new Rectangle(64, 0, 32, 16)), 2, 20);
        private static readonly Animation LEFT = new(Main.Load("/Entities/goriya.png", new Rectangle(96, 0, 32, 16)), 2, 20);

        public Goriya() : base (new Rectangle(0,0,16,16), DOWN)
        {

        }

        public override Vector2 Move(State.Game game)
        {
            Vector2 direction = game.player.Position - Position;
            float distance = direction.Length();


            const float stopDistance = 32f;

            if (Math.Abs(direction.X) > Math.Abs(direction.Y))
            {
                ActiveAnimation = direction.X > 0 ? RIGHT : LEFT;
                if (ActiveAnimation == LEFT)
                {
                    SpriteEffects flip = SpriteEffects.FlipHorizontally;
                }
            }
            else
            {
                ActiveAnimation = direction.Y > 0 ? DOWN : UP;
            }

            if (distance > stopDistance)
            {
                direction.Normalize();
                float speed = 0.5f;
                return direction * speed;
            }

            // Close enough, stop moving
            return Vector2.Zero;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}