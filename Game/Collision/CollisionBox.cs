using Microsoft.Xna.Framework;
using Game.Commands;
using System.Collections.Generic;
using System;

namespace Game.Collision;

public abstract class CollisionBox : ICollision {
    public Rectangle bounds;
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
    // public CollisionBox(int x, int y, int width, int height) : this(new Rectangle(x, y, width, height), null) {}
    public void Update() {
        if (collisionList == null) {
            return;
        }
        foreach (ICollision collision in collisionList) {
            if (CheckCollision(collision)) {
                this.OnCollision();
                collision.OnCollision();
            }
        }
    }
    public virtual void OnCollision() {
        command?.Execute();
    }
    public virtual bool CheckCollision(ICollision obj) {
        if (obj is null) return false;
        if (obj is CollisionBox && !(obj is ContainCollisionBox)) {
            if (CollisionStatics.BoxBoxCollision((CollisionBox) obj, this)) {
            }
            return CollisionStatics.BoxBoxCollision((CollisionBox) obj, this);
        } else {
            return false;
        }
    }
    public void SetPosition(int x, int y) {
        this.bounds.X = x;
        this.bounds.Y = y;
    }
    public void SetPosition(Vector2 vector) {
        this.bounds.X = (int)vector.X;
        this.bounds.Y = (int)vector.Y;
    }
}