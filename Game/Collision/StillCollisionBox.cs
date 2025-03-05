using Microsoft.Xna.Framework;
using Game.Commands;

namespace Game.Collision;

public class StillCollisionBox : CollisionBox
{
    public StillCollisionBox(Rectangle bounds, ICommand command): base(bounds, command) {}
    public StillCollisionBox(int x, int y, int width, int height, ICommand command) : base(new Rectangle(x, y, width, height), command) {}
}