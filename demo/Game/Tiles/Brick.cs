using System.Numerics;
using Microsoft.Xna.Framework.Graphics;
using Game.Graphics;

namespace Game.Tiles
{
    // We will find a use for this late
    public class Brick : ITile
    {
        private static readonly Sprite BRICK_TILE = new Sprite(Game.Load("/Tiles/wall.png"));

        public Brick()
        {

        }

        public void Update()
        {

        }
        public void Draw(SpriteBatch batch)
        {
            BRICK_TILE.Draw(batch, new Vector2(100, 100));
        }
    }
}
