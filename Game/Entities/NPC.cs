using Game.Collision;
using Game.Graphics;
using Game.State;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    public class NPC : EnemyEntity
    {
        private static readonly Animation IDLE = new(Main.Load("/Entities/NPC.png"), 2, 20);

        public NPC() : base(10, new Rectangle(0, 0, 16, 16), IDLE)
        {

        }

        public override Vector2 Move(State.Game game)
        {
            return new Vector2(0, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
