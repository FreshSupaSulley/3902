using Microsoft.Xna.Framework;
using Game.Commands;
using Game.Entities;

namespace Game.Collision;

public class ContainCollisionBox : CollisionBox {
    
    public ContainCollisionBox(Rectangle bounds, ICommand command) : base(bounds, command) {}
    public ContainCollisionBox(Rectangle bounds) : base(bounds, null) {}

    public override void OnCollision() {
        base.OnCollision();
    }
    public void Reposition(LivingEntity entity) {
        int x1 = entity.collisionBox.X;
        int x2 = entity.collisionBox.X+entity.collisionBox.Width;
        if (x1 < 0) {
            entity.Position = new Vector2(0,entity.Position.Y);
        }
        if (x2 > this.bounds.Right) {
            entity.Position = new Vector2(this.bounds.Right-this.bounds.Width, entity.Position.Y);
        }
    }
    public override bool CheckCollision(ICollision obj) {
        if (obj is CollisionBox) {
            return CollisionStatics.BoxContainsBoxCollision(this, (CollisionBox) obj);
        } else {
            return false;
        }
    }
    
}