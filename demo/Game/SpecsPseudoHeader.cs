using System;
using Microsoft.Xna.Framework.Graphics;
//This class contains information that will be specific to the particular game being implemented in this engine, such as filenames and source positions for sprite sheets
public class Specs_h
{
    public static Texture2D monoko;
    public static int spawnX = 100;
    public static int spawnY = 100;

    public static Microsoft.Xna.Framework.Rectangle monokoDefault = new Microsoft.Xna.Framework.Rectangle(432, 98, 24, 30);
    public static Microsoft.Xna.Framework.Rectangle mkFrontLeft = new Microsoft.Xna.Framework.Rectangle(410, 100, 23, 28);
    public static Microsoft.Xna.Framework.Rectangle mkFrontRight = new Microsoft.Xna.Framework.Rectangle(456, 100, 24, 27);
    public static Microsoft.Xna.Framework.Rectangle scaryDefault = new Microsoft.Xna.Framework.Rectangle(433, 168, 23, 32);
    public static Microsoft.Xna.Framework.Rectangle mkLeftLeftFoot = new Microsoft.Xna.Framework.Rectangle(414, 132, 20, 30);
    public static Microsoft.Xna.Framework.Rectangle mkLeftNeutral = new Microsoft.Xna.Framework.Rectangle(438, 132, 20, 30);
    public static Microsoft.Xna.Framework.Rectangle mkLeftRightFoot = new Microsoft.Xna.Framework.Rectangle(462, 132, 20, 30);
    public static Microsoft.Xna.Framework.Rectangle mkRightLeftFoot = new Microsoft.Xna.Framework.Rectangle(410, 68, 19, 30);
    public static Microsoft.Xna.Framework.Rectangle mkRightNeutral = new Microsoft.Xna.Framework.Rectangle(433, 68, 19, 30);
    public static Microsoft.Xna.Framework.Rectangle mkRightRightFoot = new Microsoft.Xna.Framework.Rectangle(457, 68, 19, 30);
    public static Microsoft.Xna.Framework.Rectangle mkBackNeutral = new Microsoft.Xna.Framework.Rectangle(433, 36, 24, 30);
    public static Microsoft.Xna.Framework.Rectangle mkBackLeft = new Microsoft.Xna.Framework.Rectangle(409, 36, 24, 30);
    public static Microsoft.Xna.Framework.Rectangle mkBackRight = new Microsoft.Xna.Framework.Rectangle(458, 36, 24, 30);
    public static Microsoft.Xna.Framework.Rectangle[] mkLeft = new Microsoft.Xna.Framework.Rectangle[] { mkLeftNeutral, mkLeftLeftFoot, mkLeftNeutral, mkLeftRightFoot };
    public static Microsoft.Xna.Framework.Rectangle[] mkRight = new Microsoft.Xna.Framework.Rectangle[] { mkRightNeutral, mkRightLeftFoot, mkRightNeutral, mkRightRightFoot };
    public static Microsoft.Xna.Framework.Rectangle[] mkFront = new Microsoft.Xna.Framework.Rectangle[] { monokoDefault, mkFrontLeft, monokoDefault, mkFrontRight };
    public static Microsoft.Xna.Framework.Rectangle[] mkBack = new Microsoft.Xna.Framework.Rectangle[] { mkBackNeutral, mkBackLeft, mkBackNeutral, mkBackRight };
    public static Microsoft.Xna.Framework.Rectangle[] mkAll = new Microsoft.Xna.Framework.Rectangle[] { monokoDefault, mkFrontLeft, mkFrontRight, scaryDefault, mkRightLeftFoot, mkRightNeutral, mkRightRightFoot, mkLeftLeftFoot, mkLeftNeutral, mkLeftRightFoot, mkBackLeft, mkBackNeutral, mkBackRight };
    public static int[] mkRightIndex = { 5, 4, 5, 6 };
    public static int[] mkLeftIndex = {8, 7, 8, 9 };
    public static int[] mkDown = { 0, 1, 0, 2 };
    public static int[] mkUp = { 11, 10, 11, 12 };
    public static int cycle(int i, int length)
    {
        i++;
        if(i >= length)
        {
            i = 0;
        }
        return i;
    }
}
