using Game.Commands;
using System.Collections.Generic;

namespace Game.Collision;

public interface ICollision {
    public ICommand Command {get; set;}
    public List<ICollision> CollisionList {get; set;}
    public int Type {get;}
    public void Update();
    public bool CheckCollision(ICollision obj);
    public void OnCollision();
}