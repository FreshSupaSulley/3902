using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Xna.Framework;

namespace Game.Collision;

public abstract class PushCollisionBox : ICollision
{
    Rectangle bounds;
    ICommand command;
    PushCollisionBox(Rectangle bounds, ICommand command) {
        this.bounds = bounds;
        this.command = command;
    }
    public void Update() {}
    public void CheckCollisions() {
        if (bounds.Intersects(bounds)) {
            // TODO: Add functionality
        }
    }
    public void OnCollision() {
        command.Execute(null);
    }

    public abstract bool CheckCollision();
}