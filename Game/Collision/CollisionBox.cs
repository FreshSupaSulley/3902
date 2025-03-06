using Microsoft.Xna.Framework;
using Game.Commands;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Game.Collision;

public abstract class CollisionBox : ICollision {
    public readonly Rectangle bounds;
    private ICommand command;
    public ICommand Command {
        get {return command;} 
        set {command = value;}
    }
    public List<ICollision> CollisionList {
        get {return collisionList;}
        set {collisionList = value;}
    }
    private List<ICollision> collisionList;
    public int Type {get {return type;}}
    private int type = 0;

    public CollisionBox(Rectangle bounds, ICommand command) {
        this.bounds = bounds;
        this.Command = command;
    }
    public CollisionBox(int x, int y, int width, int height, ICommand command) : this(new Rectangle(x, y, width, height), command) {}
    public CollisionBox(int x, int y, int width, int height) : this(new Rectangle(x, y, width, height), null) {}
    public void Update() {
        if (collisionList == null) {
            return;
        }
        foreach (ICollision collision in collisionList) {
            CheckCollision(collision);
        }
    }
    public void OnCollision() {
        command.Execute();
    }
    public bool CheckCollision(ICollision obj) {
        return false;
    }
}