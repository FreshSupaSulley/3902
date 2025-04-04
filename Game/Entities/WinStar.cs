using Microsoft.Xna.Framework;
using Game.Graphics;
using Game.Entities;
using Microsoft.Xna.Framework.Graphics;
using Game.State;

namespace Game.Entities
{
    public class WinStar() : Entity(new())
    {
        private static readonly Sprite SPRITE = new Sprite(Main.Load("/Misc/your.jpg"), 100, 100);

        public override void Update(State.Game game)
        {
            if (game.player.Intersects(new((int)Position.X, (int)Position.Y, SPRITE.Texture.Width, SPRITE.Texture.Height)))
            {
                game.sfx["wow"].Play();
                Main.SwitchGameState(new Win());
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            SPRITE.Draw(batch, Position);
        }
    }
}
