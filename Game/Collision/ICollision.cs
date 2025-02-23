using System.ComponentModel.Design;

namespace Game.Collision;

public interface ICollision {
    public void Update();
    public bool CheckCollision();
    public void OnCollision();
}