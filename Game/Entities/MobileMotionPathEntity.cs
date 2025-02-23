using Game.Graphics;
using Game.Path;
using Microsoft.Xna.Framework;

namespace Game.Entities;

public abstract class MobileMotionPathEntity(Vector2 position, Animation activeAnimation, IPath[] paths) : MobileEntity(position, activeAnimation)
{
	protected IPath[] paths = paths;
	protected int currentPath = 0;

	public override void Update(Game game)
	{
		if (paths[currentPath].Done)
		{
			currentPath++;
		}
		if (currentPath >= paths.Length)
		{
			currentPath = 0;
			foreach (IPath path in this.paths)
			{
				path.Reset();
			}
		}
		paths[currentPath].Update();
		this.Position = paths[currentPath].Position;
		base.Update(game);
	}
}