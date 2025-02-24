using Game.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    public class Bomb(Vector2 position) : Item(position)
    {
        private static readonly int bombDelay = 120, fireTime = 600;

        private static readonly Sprite BOMB = new(Game.Load("/Items/zelda_items.png", new(136, 0, 8, 14)));
        private static readonly Texture2D FIRE_TEX = Game.Load("/Misc/fire.png");
        private readonly Animation FIRE = new(FIRE_TEX, 2, 10);

        private int ticks;
        public bool exploded;

        public override void Use(Game game)
        {
            game.room.AddEntity(this);
        }

        public override void Update(Game game)
        {
            if (ticks++ >= bombDelay)
            {
                exploded = true;
                FIRE.Update();
            }

            // Despawn after a while
            if (ticks >= fireTime)
            {
                game.room.RemoveEntity(this);
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            if (exploded)
            {
                FIRE.Draw(batch, Position);
            }
            else
            {
                BOMB.Draw(batch, Position);
            }
        }
    }
}
