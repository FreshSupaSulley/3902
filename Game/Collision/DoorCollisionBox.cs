using Microsoft.Xna.Framework;
using Game.Commands;

namespace Game.Collision;

class DoorCollisionBox : CollisionBox {
    DoorCollisionBox(Rectangle bounds, ICommand command) : base (bounds, command) {}
    DoorCollisionBox(int x, int y, int w, int h, ICommand command) : base (x, y, w, h, command) {}
}