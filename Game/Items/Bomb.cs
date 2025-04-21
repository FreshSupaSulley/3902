using Game.Entities;
using Game.Graphics;
using Game.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    public class Bomb() : Entity(new(0, 0))
    {
        private static readonly int bombDelay = 120, fireTime = 600;

        private static readonly Sprite BOMB = new(Main.Load("/Items/zelda_items.png", new(136, 0, 8, 14)));
        private static readonly Texture2D FIRE_TEX = Main.Load("/Misc/fire.png");
        private readonly Animation FIRE = new(FIRE_TEX, 2, 10);

        private int ticks;
        public bool exploded;

        public override void Update(State.Game game)
        {
            

            foreach (Player player in game.players)
            {
                if (player.Intersects(new((int)Position.X, (int)Position.Y, BOMB.Texture.Width, BOMB.Texture.Height)))
                {
                    game.room.RemoveEntity(this);
                    player.addBomb();
                }
            }
        }

        public void useBomb(State.Game game)
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
