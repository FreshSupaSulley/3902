namespace Game.Graphics
{
    // A Sprite is a single 2D image. Sprite classes are responsible for holding their positions in their spritesheets.
    public class Sprite(int x, int y, int width, int height)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
        public int Width { get; } = width;
        public int Height { get; } = height;
    }
}