using Game.Graphics;
using Game.Path;
using Game.State;
using Microsoft.Xna.Framework;

namespace Game.Entities
{
    public class Gohma() : MobileMotionPathEntity(new(0, 0, 10, 10), IDLE, motionPaths)
    {
        private static readonly Animation IDLE = new(Main.Load("/Entities/gohma.png", new Rectangle(0, 0, 67, 16)), 4, 10);

        private static readonly IPath[] motionPaths = [
            new LinearPath(new Vector2(200,200), new Vector2(100,0), 50),
            new LinearPath(new Vector2(300,200), new Vector2(-100,0), 50),
            new LinearPath(new Vector2(200,200), new Vector2(0,100), 50),
            new LinearPath(new Vector2(200,300), new Vector2(0,-200), 100),
            new LinearPath(new Vector2(200,100), new Vector2(0,100), 50),
            new LinearPath(new Vector2(200,200), new Vector2(-100,0), 50),
            new LinearPath(new Vector2(100,200), new Vector2(100,0), 50),
        ];

        public override Vector2 Move(State.Game game)
        {
            return base.Move(game);
            // ticks++;
            // Position += new Vector2(ticks / 10 % 2 == 0 ? 1 : -1, 0);
        }
    }
}