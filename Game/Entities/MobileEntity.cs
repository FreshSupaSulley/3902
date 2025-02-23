using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Game.Entities
{
    // LivingEntity uses animations to render itself. Maybe use a better name?
    public abstract class MobileEntity : Entity
    {
        private Animation _activeAnimation;

        protected Animation ActiveAnimation
        {
            get => _activeAnimation;
            set
            {
                // Only set if its a new value
                if (_activeAnimation != value)
                {
                    _activeAnimation?.Reset();
                    _activeAnimation = value;
                }
            }
        }

        public MobileEntity(Vector2 position, Animation activeAnimation) : base(position) => ActiveAnimation = activeAnimation;

        public override void Update(Game game)
        {
            ActiveAnimation.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ActiveAnimation.Draw(spriteBatch, Position);
        }
    }
}
