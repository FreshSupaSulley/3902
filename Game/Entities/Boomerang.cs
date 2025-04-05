using Microsoft.Xna.Framework;
using Game.Graphics;
using Game.Entities;
using Microsoft.Xna.Framework.Graphics;
using System;
using Game.State;

namespace Game.Tiles
{
    public class Boomerang : LivingEntity
    {
        private static readonly Animation IDLE = new Animation(Main.Load("/Entities/boomerang.png"), 3, 30);
        private static readonly int timeAlive = 50;
        private Vector2 velocity;
        private int Direction; //0 = to top, 1 = right, 2 = bottom, 3 = left

        // Used to track when it should despawn
        private int ticksAlive;

        public Boomerang(Vector2 position, int direction) : base(new Rectangle(0, 0, 10, 10), IDLE)
        {
            Position = position;
            Direction = direction;
        }

        public override Vector2 Move(State.Game game)
        {
            if (ticksAlive++ > timeAlive)
            {
                game.room.RemoveEntity(this);
            }
            switch (Direction)
            {
                case 0:
                    velocity = new(0, -1);
                    break;
                case 1:
                    velocity = new(1, 0);
                    break;
                case 2:
                    velocity = new(0, 1);
                    break;
                case 3:
                    velocity = new(-1, 0);
                    break;
            }
            return velocity;
        }

        public override void Draw(SpriteBatch batch)
        {
            IDLE.Draw(batch, Position);
        }
    }
}
