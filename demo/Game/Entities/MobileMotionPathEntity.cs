using Game.Graphics;
using Game.Path;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Entities;

public abstract class MobileMotionPathEntity : MobileEntity {
	protected IPath[] paths;
	protected int currentPath = 0;
	public MobileMotionPathEntity(Vector2 position, Animation activeAnimation, IPath[] paths) : base(position, activeAnimation) {
		this.paths = paths;
	}
    public override void Update()
    {
		if (paths[currentPath].Done) {
			currentPath++;
		}
		if (currentPath > paths.Length) {
			currentPath = 0;
			foreach (IPath path in this.paths) {
				path.Reset();
			}
		}
		paths[currentPath].Update();
		this.Position = paths[currentPath].Position;
		System.Console.WriteLine(currentPath);
        base.Update();
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}