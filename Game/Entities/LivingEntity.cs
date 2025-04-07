using Game.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Collision;
using System.Xml.Serialization;
using Game.State;
using Game.Util;
using System;
using Game.Items;

namespace Game.Entities
{
    // LivingEntitys have active animations and collisions handled for them
    public abstract class LivingEntity : Entity
    {
        [XmlIgnore]
        public readonly Rectangle collisionBox;
        [XmlIgnore]
        private Animation _activeAnimation;

        // Health value
        [XmlIgnore]
        protected int health;
        
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

        // Returns the health
        public int GetHealth() => health;

        // Damage the entity
        public virtual void Inflict(State.Game game, int damage)
        {
            int completed_damage = damage > health ? health : damage;
            
            if ((health -= damage) <= 0)
            {
                OnDeath(game);
            }
            if (health < 0) {
                health = 0;
            } 
            if (completed_damage > 0) {
                Vector2 variation = RNG.RandomVector2(-5,5,-5,5);
                InGameMessage.messages.Add(new InGameMessage("-" + completed_damage, new Vector2(base.Position.X + 30, base.Position.Y)+variation, 100, 0.25f));
            }
        }

        // By default, an entity dying just removes it from the world and plays a dumb shit sound effect
        public virtual void OnDeath(State.Game game)
        {
            game.sfx["ding"].Play();
            game.room.RemoveEntity(this);
        }

        public LivingEntity(int health, Rectangle box, Animation activeAnimation) : base(new())
        {
            this.health = health;
            collisionBox = box;
            ActiveAnimation = activeAnimation;
        }

        public bool Intersects(Rectangle rectangle)
        {
            return collisionBox.Intersects(new(rectangle.X - (int)Position.X, rectangle.Y - (int)Position.Y, rectangle.Width, rectangle.Height));
            // return new Rectangle((int)Position.X + collisionBox.X, (int)Position.Y + collisionBox.Y, this.collisionBox.X + this.collisionBox.Width, this.collisionBox.Y + this.collisionBox.Height))).;
        }

        // Subclasses need to return their velocities
        public abstract Vector2 Move(State.Game game);

        public override void Update(State.Game game)
        {
            ActiveAnimation.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ActiveAnimation.Draw(spriteBatch, Position);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            ActiveAnimation.Draw(spriteBatch, Position, color);
        }
    }
}
