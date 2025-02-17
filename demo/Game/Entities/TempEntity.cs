using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Entities
{
    //Temporary entity, an entity that appears on screen for a set amount of time and then disappears
    internal class TempEntity : Entity
    {
        private Texture2D texture;
        float opacity = 1.0f;
        Vector2 position;
        public TempEntity(Texture2D txt, Vector2 v) : base(v)
        {
            texture = txt;
            position = v;
        }
        public override void Update()
        {
            opacity *= 0.9f;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, TempBuffer.powLength, TempBuffer.powHeight), Color.White * opacity);
        }
    }
}
