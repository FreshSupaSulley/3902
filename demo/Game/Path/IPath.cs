using Microsoft.Xna.Framework;

namespace Game.Path;

public interface IPath {
	public bool Done {get;}
	public Vector2 Position {get;}
	void Update();
	void Reset();
}