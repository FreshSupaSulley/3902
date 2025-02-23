using Game.Graphics;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    public class Dragon(Vector2 Position) : MobileEntity(Position, IDLE)
    {
        private static readonly Animation IDLE = new(Game.Load("/Entities/Dragon/dragon.png"), 4, 10);
        private static readonly Animation HURT = new(Game.Load("/Entities/Dragon/hurt.png"), 1, 1);

        private int ticks;

        public override void Update(Game game)
        {
            base.Update(game);

            if(ticks++ % 60 == 0)
            {
                game.room.AddEntity(new Fireball(Position));
            }
            Position += new Vector2(0, ticks / 10 % 2 == 0 ? 1 : -1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
