using System;
using Game.Graphics;
using Game.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game.Entities;

namespace Game.Items
{
    public class Banana(Vector2 position) : Item(position)
    {
        private static readonly Sprite SPRITE = new(Main.Load("/Items/banana.png"));
        private readonly int TICKS_ALIVE = 120;
        private readonly int DISTANCE = 100;

        private readonly int XCORRECTION = -10;
        private readonly int YCORRECTION = -2;

        private Vector2 startPos, velocity;
        private int ticks = 0;

        public override void Update(State.Game game)
        {
            Position = startPos + velocity * (float)Math.Sin(ticks * Math.PI / TICKS_ALIVE) * DISTANCE;
            if (ticks++ >= TICKS_ALIVE)
            {
                game.room.RemoveEntity(this);
            }
            for(int i = 0; i < game.room.gameObjects.Count; i++){
                Entity e = game.room.gameObjects[i];
                if(e is LivingEntity){
                    LivingEntity le = (LivingEntity)e;
                   if(!(le is Player)){
                       if(Math.Pow(Position.X + XCORRECTION - le.Position.X, 2) + Math.Pow(Position.Y + YCORRECTION - le.Position.Y, 2) < 49){

                            le.Inflict(game, 1);
                      }
                  }
                }
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(SPRITE.Texture, Position, null, Color.White, MathHelper.ToRadians(ticks * 12), new Vector2(5f / 2, 8f / 2), new Vector2(1), SpriteEffects.None, 0f);
        }

        public override void Use(State.Game game)
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
