namespace Game.Collision;

public class CollisionStatics {
    public static bool BoxBoxCollision(CollisionBox a, CollisionBox b) {
        return a.bounds.Intersects(b.bounds);
    }
    public static bool BoxContainsBoxCollision(ContainCollisionBox a, CollisionBox b) {
        return !a.bounds.Contains(b.bounds);
    }
}