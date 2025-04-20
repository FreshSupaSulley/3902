using System;
using Game.Entities;
using Game.Graphics;
using Game.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Items
{
    // Hearts dont do anything yet
    public class Key() : Entity(new(0, 0))
    {
        private static readonly Sprite SPRITE = new(Main.Load("/Items/key.png"));

        public override void Update(State.Game game)
        {
            foreach(Player player in game.players){
              if (player.Intersects(new((int)Position.X, (int)Position.Y, SPRITE.Texture.Width, SPRITE.Texture.Height)))
             {
                    game.sfx["fart"].Play();
                    game.room.RemoveEntity(this);
                    player.addKey();
                }
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            SPRITE.Draw(batch, Position);
        }
    }
}
