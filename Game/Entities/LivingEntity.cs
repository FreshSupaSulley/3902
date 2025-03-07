using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Collision;

namespace Game.Entities
{
    // LivingEntitys have active animations and collisions handled for them
    public abstract class LivingEntity : Entity
    {
        public readonly CollisionBox collisionBox;
        private Animation _activeAnimation;

        // Used for collision
        public Vector2 Velocity { get; private set; }

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
        private Vector2 position;
        public override Vector2 Position {
            get{return position;}
            set{
                position = value;
                collisionBox.SetPosition(position);
            }
        }


        public LivingEntity(CollisionBox box, Vector2 position, Animation activeAnimation) : base(position)
        {
            collisionBox = box;
            ActiveAnimation = activeAnimation;
        }

        // Subclasses need to return their velocities
        public abstract Vector2 Move(Game game);

        public override sealed void Update(Game game)
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
