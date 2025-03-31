using Microsoft.Xna.Framework;
using Game.Graphics;
using Game.Entities;
using Microsoft.Xna.Framework.Graphics;
using System;
using Game.State;

namespace Game.Tiles
{
    public class Fireball(Vector2 position) : Entity(position)
    {
        private static readonly Sprite dragon_projectaile = new Sprite(Main.Load("/Entities/Dragon/projectile.png"));
        private static readonly float speed = 1f;
        private static readonly int timeAlive = 30;

        // Used to track when it should despawn
        private int ticksAlive;

        public override void Update(World game)
        {
            Position -= new Vector2(speed, 0);
            if (ticksAlive++ > timeAlive)
            {
                game.room.RemoveEntity(this);
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            dragon_projectaile.Draw(batch, Position);
        }
    }
}
