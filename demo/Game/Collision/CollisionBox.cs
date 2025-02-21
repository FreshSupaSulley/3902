using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Game.Collision;

public abstract class CollisionBox : ICollision {
    Rectangle bounds;
    CollisionBox(Rectangle bounds) {
        this.bounds = bounds;
    }
    public void Update() {}
    public void CheckCollisions() {
        if (bounds.Intersects(bounds)) {
            // TODO: Add functionality
        }
    }
    public void OnCollision() {}
}