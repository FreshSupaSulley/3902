using Game.Commands;
using Microsoft.Xna.Framework;

namespace Game.Collision;

public class PushCollisionBox : CollisionBox
{
    public PushCollisionBox(Rectangle bounds, ICommand command) : base(bounds, command) {}
    public PushCollisionBox(int x, int y, int width, int height, ICommand command) : base(new Rectangle(x, y, width, height), command) {}
}