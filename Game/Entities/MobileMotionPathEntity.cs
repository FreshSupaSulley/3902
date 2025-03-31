using Game.Graphics;
using Game.Path;
using Game.State;
using Microsoft.Xna.Framework;

namespace Game.Entities;

public abstract class MobileMotionPathEntity(Rectangle collisionBox, Animation activeAnimation, IPath[] paths) : LivingEntity(collisionBox, activeAnimation)
{
	protected IPath[] paths = paths;
	protected int currentPath = 0;

	public override Vector2 Move(World game)
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