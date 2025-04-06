using Game.Collision;
using Game.Graphics;
using Game.State;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game.Entities
{
    public class Trap : LivingEntity
    {
        private static readonly Animation IDLE = new(Main.Load("/Entities/trap.png"), 1, 20);

        private Vector2 velocity = Vector2.Zero;
        private bool vMoving = false;
        private bool hMoving = false;
        private bool returning = false;
        private const float Speed = 1f;

        public Trap() : base(new Rectangle(0, 0, 16, 16), IDLE)
        {

        }

        public override Vector2 Move(State.Game game)
        {
            // Calculate direction to the player
            Vector2 toPlayer = game.player.Position - Position + new Vector2(game.player.collisionBox.Width / 2, game.player.collisionBox.Height / 2);
            float distanceToPlayer = toPlayer.Length();
            float playerX = game.player.Position.X + game.player.collisionBox.Width / 2;
            float playerY = game.player.Position.Y + game.player.collisionBox.Height / 2;

            if (playerX >= Position.X && playerX <= Position.X + collisionBox.Width)
            {
                vMoving = true;
                hMoving = false;
            }
            else if (playerY >= Position.Y && playerY <= Position.Y + collisionBox.Height)
            {
                vMoving = false;
                hMoving = true;
            }

            if (vMoving == true)
            {
                //up down
                if (Position.Y < 80)
                {
                    velocity = new Vector2(0, 1);

                    if (Position.Y == 74)
                    {
                        velocity = Vector2.Zero;
                        returning = true;
                    }
                } else if (Position.Y > 80)
                {
                    velocity = new Vector2(0, -1);

                    if (Position.Y == 86)
                    {
                        velocity = Vector2.Zero;
                        returning = true;
                    }
                }
            }
            else if(hMoving == true) { 
                // left right
                if (Position.X < 112)
                {
                    velocity = new Vector2(1, 0);

                    if (Position.X == 106)
                    {
                        velocity = Vector2.Zero;
                        returning = true;
                    }
                }
                else if (Position.X > 118)
                {
                    velocity = new Vector2(-1, 0);

                    if (Position.X == 88)
                    {
                        velocity = Vector2.Zero;
                        returning = true;
                    }
                }
            }

            if (returning == true && vMoving == true)
            {
                //up down
                if (Position.Y <= 74)
                {
                    velocity = new Vector2(0, -1);
                    if (Position.Y == 32)
                    {
                        vMoving = false;
                        returning = false;
                    }
                }
                if (Position.Y >= 86)
                {
                    velocity = new Vector2(0, 1);
                    if (Position.Y == 128)
                    {
                        vMoving = false;
                        returning = false;
                    }
                }
            }
            if(returning == true && hMoving== true) { 
                // left right
                if (Position.X <= 106)
                {
                    velocity = new Vector2(-1, 0);
                    if (Position.X == 32)
                    {
                        hMoving = false;
                        returning = false;
                    }
                }
                if (Position.X >= 112)
                {
                    velocity = new Vector2(1, 0);
                    if (Position.X == 128)
                    {
                        hMoving = false;
                        returning = false;
                    }
                }
            }
            return velocity;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
