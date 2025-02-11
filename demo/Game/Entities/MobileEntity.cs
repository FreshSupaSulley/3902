using System.Numerics;
using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Entities
{
    // LivingEntity uses animations to render itself. Maybe use a better name?
    public abstract class MobileEntity : Entity
    {
        protected Animation activeAnimation { get; set; }

        public MobileEntity(Vector2 position, Animation activeAnimation) : base(position)
        {
            this.activeAnimation = activeAnimation;
        }

        public override void Update()
        {
            activeAnimation.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            activeAnimation.Draw(spriteBatch, Position);
        }
    }
}
