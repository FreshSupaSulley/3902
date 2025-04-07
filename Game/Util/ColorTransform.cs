using Microsoft.Xna.Framework;

public static class ColorTransform {
    public static Color Add(Color color, byte r, byte g, byte b, byte a) {
        Color newColor = color;
        newColor.R += r;
        newColor.G += g;
        newColor.B += b;
        newColor.A += a;
        return newColor;
    }
    public static Color Add(Color color, int r, int g, int b) {
        return Add(color, (byte) r, (byte) g, (byte) b);
    }
    public static Color Add(Color color, byte r, byte g, byte b) {
        return Add(color, r, g, b, 0);
    }
}
