using Game.Entities;

namespace Game.Commands
{
    internal class PlayerMovementCommand : PlayerCommand
    {
        int direction;
        int ll; //latitude or longitude: 0 for right-left movement, 1 for up-down movement

        public PlayerMovementCommand(Player p, int d, int l) : base(p)
        {
            direction = d;
            ll = l;
        }

        public override void Execute()
        {
            //If ll = 0, addition to y-position will be 0; if ll = 1, addition to x position will be 1
            this.player.Position = new System.Numerics.Vector2(this.player.Position.X + (1 - ll) * direction * player.speed, this.player.Position.Y + (ll) * direction * player.speed);
            this.player.animate(direction, ll);
        }
    }
}
