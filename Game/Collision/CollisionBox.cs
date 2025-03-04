using Microsoft.Xna.Framework;

namespace Game.Collision;

public class CollisionBox(int offsetX, int offsetY, int width, int height)
{
    public readonly Rectangle bounds = new(offsetX, offsetY, width, height);
    public CollisionBox(int width, int height) : this(0, 0, width, height) { }
    // public void CheckCollisions(LivingEntity entity, CollisionBox box) => bounds.Intersects(box.bounds);
}