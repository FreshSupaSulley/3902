using Microsoft.Xna.Framework;
using System;
public static class RNG {
    private static Random random = new Random();

    public static Vector2 RandomVector2(float minX, float maxX, float minY, float maxY)
    {
        float x = (float)(random.NextDouble() * (maxX - minX) + minX);
        float y = (float)(random.NextDouble() * (maxY - minY) + minY);
        return new Vector2(x, y);
    }

}