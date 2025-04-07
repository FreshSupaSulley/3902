using Game.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Entities
{
    /// Enemies have a passive but active hitbox that when they collide with the player, they inflict damage
    public abstract class EnemyEntity(int health, int damage, Rectangle box, Animation activeAnimation) : LivingEntity(health, box, activeAnimation)
    {
        private readonly int damage = damage;

        /// Not specifying a damage amount uses the base health of the enemy as its value for inflicting damage
        public EnemyEntity(int health, Rectangle box, Animation activeAnimation) : this(health, health, box, activeAnimation) { }

        public override void Update(State.Game game)
        {
            base.Update(game);
            // Add itself as a hitbox
            game.room.AddHitbox(new(damage, this, base.collisionBox));
        }

        public int GetDamage()
        {
            return damage;
        }
    }
}
