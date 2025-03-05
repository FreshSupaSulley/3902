using Game.Commands;

namespace Game.Collision;

public interface ICollision {
    public ICommand Command {get; set;}
    public void Update();
    public bool CheckCollision();
    public void OnCollision();
}