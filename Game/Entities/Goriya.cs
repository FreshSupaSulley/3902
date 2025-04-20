using Game.Graphics;
using Game.Path;
using Game.State;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game.Entities
{
    public class Goriya : EnemyEntity
    {

        private static readonly Texture2D GORIYA_TEXTURE = Main.Load("/Entities/goriya.png");

        private static readonly Animation DOWN = new(Main.Load("/Entities/goriya.png", new Rectangle(0, 0, 32, 16)), 2, 20);
        private static readonly Animation UP = new(Main.Load("/Entities/goriya.png", new Rectangle(32, 0, 32, 16)), 2, 20);
        private static readonly Animation RIGHT = new(Main.Load("/Entities/goriya.png", new Rectangle(64, 0, 32, 16)), 2, 20);
        private static readonly Animation LEFT = new(Main.Load("/Entities/goriya.png", new Rectangle(96, 0, 32, 16)), 2, 20);

        public Goriya() : base (10,new Rectangle(0,0,16,16), DOWN)
        {

        }
        private int ticks;

        public override Vector2 Move(State.Game game)
        {
            Vector2 direction = new Vector2();
            int boomerang_direction = 0;
            foreach(Player player in game.players){
                direction = player.Position - Position;
            }
            float distance = direction.Length();

            const float stopDistance = 40f;

            if (Math.Abs(direction.X) > Math.Abs(direction.Y))
            {
                ActiveAnimation = direction.X > 0 ? RIGHT : LEFT;
                boomerang_direction = ActiveAnimation == LEFT ? 3 : 1;
            }
            else
            {
                ActiveAnimation = direction.Y > 0 ? DOWN : UP;
                boomerang_direction = ActiveAnimation == DOWN ? 2 : 0;
            }

            if (ticks++ % 60 == 0)
            {
                game.room.AddEntity(new Boomerang(Position, boomerang_direction));
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