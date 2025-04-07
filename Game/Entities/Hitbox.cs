using Microsoft.Xna.Framework;

namespace Game.Entities
{
    public class Hitbox(int damage, Entity owner, Rectangle collisionBox)
    {
        private readonly int damage = damage;
        private readonly Entity owner = owner;
        private readonly Rectangle collisionBox = collisionBox;

        public int GetDamage() => damage;
        public Entity GetEntity() => owner;
        public Rectangle GetCollisionBox() => new((int)owner.Position.X + collisionBox.X, (int)owner.Position.Y + collisionBox.Y, collisionBox.Width, collisionBox.Height);
    }
}
