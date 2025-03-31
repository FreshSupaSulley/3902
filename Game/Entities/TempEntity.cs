using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.State;
using Game.Util;

namespace Game.Entities
{
    //Temporary entity, an entity that appears on screen for a set amount of time and then disappears
    internal class TempEntity(Texture2D txt, Vector2 v) : Entity(v * Main.BASE_TO_WINDOW)
    {
        private readonly Texture2D texture = txt;
        float opacity = 1.0f;

        public override void Update(State.Game game)
        {
            opacity *= 0.9f;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, TempBuffer.powLength, TempBuffer.powHeight), Color.White * opacity);
        }
    }
}
