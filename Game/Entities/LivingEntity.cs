using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Collision;
using System.Xml.Serialization;
using Game.State;

namespace Game.Entities
{
    // LivingEntitys have active animations and collisions handled for them
    public abstract class LivingEntity : Entity
    {
        [XmlIgnore]
        public readonly Rectangle collisionBox;
        [XmlIgnore]
        private Animation _activeAnimation;

        // Used for collision
        [XmlIgnore]
        public Vector2 Velocity { get; private set; }

        [XmlIgnore]
        public Animation ActiveAnimation
        {
            get => _activeAnimation;
            set
            {
                // Only set if it's a new value
                if (_activeAnimation != value)
                {
                    _activeAnimation?.Reset();
                    _activeAnimation = value;
                }
            }
        }

        public LivingEntity(Rectangle box, Animation activeAnimation) : base(new())
        {
            collisionBox = box;
            ActiveAnimation = activeAnimation;
        }

        // Subclasses need to return their velocities
        public abstract Vector2 Move(World game);

        public override sealed void Update(World game)
        {
            Velocity = Move(game);
            ActiveAnimation.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ActiveAnimation.Draw(spriteBatch, Position);
        }
    }
}
