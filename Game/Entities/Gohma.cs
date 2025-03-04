using Game.Graphics;
using Game.Path;
using Microsoft.Xna.Framework;

namespace Game.Entities
{
    public class Gohma(Vector2 position) : MobileMotionPathEntity(new(10, 10), position, IDLE, motionPaths)
    {
        private int ticks;
        private static readonly Animation IDLE = new(Game.Load("/Entities/gohma.png", new Rectangle(0, 0, 192, 16)), 4, 10);

        private static readonly IPath[] motionPaths = [
            new LinearPath(new Vector2(200,200), new Vector2(100,0), 50),
            new LinearPath(new Vector2(300,200), new Vector2(-100,0), 50),
            new LinearPath(new Vector2(200,200), new Vector2(0,100), 50),
            new LinearPath(new Vector2(200,300), new Vector2(0,-200), 100),
            new LinearPath(new Vector2(200,100), new Vector2(0,100), 50),
            new LinearPath(new Vector2(200,200), new Vector2(-100,0), 50),
            new LinearPath(new Vector2(100,200), new Vector2(100,0), 50),
        ];
        
        public override Vector2 Move(Game game)
        {
            return base.Move(game);
            // ticks++;
            // Position += new Vector2(ticks / 10 % 2 == 0 ? 1 : -1, 0);
        }
    }
}