using System;
using Game.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    public class Boomerang(Vector2 position) : Item(position)
    {
        private static readonly Sprite SPRITE = new(Game.Load("/Items/banana.png"));
        private readonly int TICKS_ALIVE = 120;
        private readonly int DISTANCE = 100;

        private Vector2 startPos, velocity;
        private int ticks = 0;

        public override void Update(Game game)
        {
            Position = startPos + velocity * (float)Math.Sin(ticks * Math.PI / TICKS_ALIVE) * DISTANCE;
            if (ticks++ >= TICKS_ALIVE)
            {
                game.room.RemoveEntity(this);
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(SPRITE.Texture, Position, null, Color.White, MathHelper.ToRadians(ticks * 12), new Vector2(5f / 2, 8f / 2), new Vector2(1), SpriteEffects.None, 0f);
        }

        public override void Use(Game game)
        {
            // Launch in direction of player
            switch (game.player.GetDirection())
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
            startPos = new Vector2(Position.X, Position.Y);
            game.room.AddEntity(this);
        }
    }
}
