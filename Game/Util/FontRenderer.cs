using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Util
{
    public static class FontRenderer
    {
        public static void Text(string output, SpriteBatch batch, Vector2 position)
        {
            batch.DrawString(Main.INSTANCE.font, output, position, Color.Black, 0, Main.INSTANCE.font.MeasureString(output) / 2, 1, SpriteEffects.None, 0.5f);
        }
    }
}
