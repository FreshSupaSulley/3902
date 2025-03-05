using Microsoft.Xna.Framework;
using Game.Commands;

namespace Game.Collision;

public abstract class CollisionBox : ICollision {
    public readonly Rectangle bounds;
    private ICommand command;
    public ICommand Command {
        get {return command;} 
        set {command = value;}
    }

    public CollisionBox(Rectangle bounds, ICommand command) {
        this.bounds = bounds;
        this.Command = command;
    }
    public CollisionBox(int x, int y, int width, int height, ICommand command) : this(new Rectangle(x, y, width, height), command) {}
    public void Update() {

    }
    public void OnCollision() {
        command.Execute();
    }
    public bool CheckCollision() {
        return false;
    }
}