using System.Data.Common;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public static class DebugTools {
    public static void DrawRect(SpriteBatch batch, Rectangle bounds, Color color) {
        Texture2D _rectTexture = new Texture2D(Main.INSTANCE.GraphicsDevice, 1, 1);
        _rectTexture.SetData(new Color[] { Color.White });
        batch.Draw(_rectTexture, bounds, color);
    }
    public static void DrawRect(SpriteBatch batch, Rectangle bounds) {
        DrawRect(batch, bounds, new Color(Color.Red, 0.5f));
    }
}