using Game.Collision;
using Game.Graphics;
using Game.Path;
using Microsoft.Xna.Framework;

namespace Game.Entities;

public abstract class MobileMotionPathEntity(CollisionBox box, Vector2 position, Animation activeAnimation, IPath[] paths) : LivingEntity(box, position, activeAnimation)
{
	protected IPath[] paths = paths;
	protected int currentPath = 0;

	public override Vector2 Move(Game game)
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
		return paths[currentPath].Position;
	}
}