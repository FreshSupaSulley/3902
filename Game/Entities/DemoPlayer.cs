using System.Numerics;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    // An Entity is an object in the game with a position
    public class DemoPlayer : LivingEntity
    {
        public DemoPlayer(Vector2 position) : base(position, AnimationRegistry.Run)
        {
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
