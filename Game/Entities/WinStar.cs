using Microsoft.Xna.Framework;
using Game.Graphics;
using Game.Entities;
using Microsoft.Xna.Framework.Graphics;
using Game.State;

namespace Game.Entities
{
    public class WinStar() : Entity(new())
    {
        private static readonly Sprite SPRITE = new Sprite(Main.Load("/Misc/your.png"));

        public override void Update(State.Game game)
        {
            foreach(Player player in game.players){
                if (player.Intersects(new((int)Position.X, (int)Position.Y, SPRITE.Texture.Width, SPRITE.Texture.Height)))
                {
                    game.sfx["wow"].Play();
                    Main.SwitchGameState(new Win());
                    Main.uiManager.ChangeUIState("win");
                }
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            SPRITE.Draw(batch, Position);
        }
    }
}
